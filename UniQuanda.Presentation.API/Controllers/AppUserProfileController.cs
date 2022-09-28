using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Application.CQRS.Queries.AppUser.Profile.AppUserProfile;
using UniQuanda.Infrastructure.Helpers;

namespace UniQuanda.Presentation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "user")]
public class AppUserProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppUserProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get data of AppUser for update profile settings from Db
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppUserProfileResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("settings")]
    public async Task<IActionResult> GetAppUserForEditProfileSettings(
        CancellationToken ct)
    {
        var idAppUser = JwtTokenHelper.GetAppUserIdFromToken(User);
        if (idAppUser == null)
            return Unauthorized();
        var query = new AppUserProfileQuery(idAppUser.Value);
        var appUserData = await _mediator.Send(query, ct);
        if (appUserData == null)
            return NotFound();
        return Ok(appUserData);
    }

    /// <summary>
    ///     Update AppUser profile settings in Db
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPut("settings")]
    [RequestSizeLimit(21 * 1024 * 1024)]
    public async Task<IActionResult> UpdateAppUserProfileSettings(
        [FromForm] UpdateAppUserProfileRequestDTO request,
        CancellationToken ct)
    {
        var idAppUser = JwtTokenHelper.GetAppUserIdFromToken(User);
        if (idAppUser == null)
            return Unauthorized();

        var command = new UpdateAppUserProfileCommand(request, idAppUser.Value);
        var result = await _mediator.Send(command, ct);
        return result.IsSuccessful switch
        {
            null => NotFound(),
            false => Conflict(),
            true => Ok(new { avatarUrl = result.AvatarUrl })
        };
    }
}