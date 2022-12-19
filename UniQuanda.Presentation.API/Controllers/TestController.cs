using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Test.GetAutomaticTest;
using UniQuanda.Core.Application.CQRS.Queries.Search.Search;

namespace UniQuanda.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "user")]
public class TestController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestController(IMediator mediator)
    {
        _mediator = mediator;
    }

		/// <summary>
    /// Gets random questions from given tags for automatic test
    /// </summary>
    [HttpGet("automatic")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAutomaticTestResponseDTO))]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> GenerateAutomaticTest(
        [FromQuery] GetAutomaticTestRequestDTO request,
        CancellationToken ct)
    {
        var query = new GetAutomaticTestQuery(request);
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }
}