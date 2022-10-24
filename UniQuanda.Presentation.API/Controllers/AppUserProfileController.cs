using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Application.CQRS.Queries.AppUser.Profile.GetAppUserProfileSettings;
using UniQuanda.Core.Application.CQRS.Queries.Profile.GetProfile;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Presentation.API.Extensions;

namespace UniQuanda.Presentation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppUserProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppUserProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Checks if given e-mail address and given nickname are already used by any users
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProfileResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-profile")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProfile(
        [FromQuery] GetProfileRequestDTO request,
        CancellationToken ct)
    {
        var query = new GetProfileQuery(request);
        var profile = await _mediator.Send(query, ct);
        return profile != null ? Ok(profile) : NotFound();
    }

    /// <summary>
    ///     Get data of AppUser for update profile settings from Db
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAppUserProfileSettingsResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("settings")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetAppUserProfileSettings(
        CancellationToken ct)
    {
        var query = new GetAppUserProfileSettingsQuery(User.GetId()!.Value));
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
    [RequestSizeLimit(21 * (int)ByteSizeEnum.MegaByte)]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> UpdateAppUserProfileSettings(
        [FromForm] UpdateAppUserProfileRequestDTO request,
        CancellationToken ct)
    {
        var command = new UpdateAppUserProfileCommand(request, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result.AppUserUpdateStatus switch
        {
            AppUserUpdateStatusEnum.AppUserNotExist => NotFound(),
            AppUserUpdateStatusEnum.NickNameIsUsed => Conflict(new { isNickNameUsed = true }),
            AppUserUpdateStatusEnum.NotSuccessful => Conflict(new { isNickNameUsed = false }),
            AppUserUpdateStatusEnum.Successful => Ok(new { avatarUrl = result.AvatarUrl })
        };
    }
}