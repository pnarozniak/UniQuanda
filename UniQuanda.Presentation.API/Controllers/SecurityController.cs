using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Security.UpdateUserMainEmail;
using UniQuanda.Core.Application.CQRS.Queries.Security.GetUserEmails;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Presentation.API.Extensions;

namespace UniQuanda.Presentation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "user")]
public class SecurityController : ControllerBase
{
    private readonly IMediator _mediator;

    public SecurityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get all emails connected with User
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserEmailsReponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-user-emails")]
    public async Task<IActionResult> GetUserEmails(CancellationToken ct)
    {
        var command = new GetUserEmailsQuery(User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>
    ///     Update main email assinged to user
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPut("update-main-email")]
    public async Task<IActionResult> UpdateUserMainEmail([FromBody] UpdateUserMainEmailRequestDTO request, CancellationToken ct)
    {
        var command = new UpdateUserMainEmailCommand(request, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result switch
        {
            UpdateResultOfEmailOrPasswordEnum.UserNotExist => NotFound(),
            UpdateResultOfEmailOrPasswordEnum.EmailIsNotConnected => Conflict(new { Status = UpdateUserMainEmailResponseDTO.EmailIsNotConnectedWithUser }),
            UpdateResultOfEmailOrPasswordEnum.InvalidPassword => Conflict(new { Status = UpdateUserMainEmailResponseDTO.PasswordIsInvalid }),
            UpdateResultOfEmailOrPasswordEnum.NotSuccessful => Conflict(new { Status = UpdateUserMainEmailResponseDTO.UpdateError }),
            UpdateResultOfEmailOrPasswordEnum.Successful => NoContent()
        };
    }
}

