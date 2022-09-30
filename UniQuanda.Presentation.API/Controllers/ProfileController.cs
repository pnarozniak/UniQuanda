using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Queries.Profile.GetProfile;

namespace UniQuanda.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Checks if given e-mail address and given nickname are already used by any users
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProfileResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-profile")]
    public async Task<IActionResult> GetProfile(
        [FromQuery] GetProfileRequestDTO request,
        CancellationToken ct)
    {
        var query = new GetProfileQuery(request);
        var profile = await _mediator.Send(query, ct);
        return profile != null ? Ok(profile) : NotFound();
    }
}