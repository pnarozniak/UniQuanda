using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Queries.Security.GetUserEmails;
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
        var idAppUser = User.GetId();
        if (idAppUser == null)
            return Unauthorized();

        var command = new GetUserEmailsQuery(idAppUser.Value);
        var result = await _mediator.Send(command, ct);
        if (result is null)
            return NotFound();
        return Ok(result);

    }
}

