using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Application.CQRS.Queries.AppUser.Profile.GetAppUserProfileSettings;
using UniQuanda.Core.Application.CQRS.Queries.Profile.GetAnswersProfile;
using UniQuanda.Core.Application.CQRS.Queries.Profile.GetProfile;
using UniQuanda.Core.Application.CQRS.Queries.Profile.GetQuestionsProfile;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Presentation.API.Extensions;
using UniQuanda.Core.Application.CQRS.Queries.Profile.GetAnswersProfile;
using UniQuanda.Core.Domain.Utils;

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
    [Authorize(Roles = AppRole.User)]
    public async Task<IActionResult> GetAppUserProfileSettings(
        CancellationToken ct)
    {
        var query = new GetAppUserProfileSettingsQuery(User.GetId()!.Value);
        var appUserData = await _mediator.Send(query, ct);
        if (appUserData == null)
            return NotFound();
        return Ok(appUserData);
    }

    /// <summary>
    ///     Update AppUser profile settings in Db
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateAppUserProfileResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(UpdateAppUserProfileResponseDTO))]
    [HttpPut("settings")]
    [RequestSizeLimit(21 * (int)ByteSizeEnum.MegaByte)]
    [Authorize(Roles = AppRole.User)]
    public async Task<IActionResult> UpdateAppUserProfileSettings(
        [FromForm] UpdateAppUserProfileRequestDTO request,
        CancellationToken ct)
    {
        var command = new UpdateAppUserProfileCommand(request, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);

        return result.UpdateStatus switch
        {
            AppUserProfileUpdateResultEnum.ContentNotExist => NotFound(),
            AppUserProfileUpdateResultEnum.Successful => Ok(result),
            _ => Conflict(result)
        };
    }

    /// <summary>
    ///     Gets user's questions on profile using paging
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetQuestionsProfileResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("questions")]
    [AllowAnonymous]
    public async Task<IActionResult> GetQuestionsOnProfile([FromQuery] GetQuestionsProfileRequestDTO request, CancellationToken ct)
    {
        var query = new GetQuestionsProfileQuery(request);
        var result = await _mediator.Send(query, ct);
        return result.Questions != null ? Ok(result) : NotFound();
    }

    /// <summary>
    ///     Gets user's answers on profile using paging
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAnswersProfileResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("answers")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAnswersOnProfile([FromQuery] GetAnswersProfileRequestDTO request, CancellationToken ct)
    {
        var query = new GetAnswersProfileQuery(request);
        var result = await _mediator.Send(query, ct);
        return result.Answers != null ? Ok(result) : NotFound();
    }
}