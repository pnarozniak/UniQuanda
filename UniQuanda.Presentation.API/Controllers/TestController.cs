using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Test.FinishTest;
using UniQuanda.Core.Application.CQRS.Commands.Test.GenerateTest;
using UniQuanda.Core.Application.CQRS.Commands.Test.GetUserTests;
using UniQuanda.Core.Application.CQRS.Queries.Test.GetTest;
using UniQuanda.Core.Application.CQRS.Queries.Test.GetUserTests;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Presentation.API.Extensions;

namespace UniQuanda.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRole.User)]
public class TestController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestController(IMediator mediator)
    {
        _mediator = mediator;
    }

		/// <summary>
    /// Generates test for user from given tags
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTestResponseDTO))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> GenerateTest(
      [FromBody] GenerateTestRequestDTO request, CancellationToken ct)
    {
        var query = new GenerateTestCommand(User.GetId()!.Value, request);
        var result = await _mediator.Send(query, ct);
        return result is null ? Conflict() : Ok(result);
    }

		/// <summary>
    /// Gets test by id
    /// </summary>
    [HttpGet("{idTest:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTestResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTest([FromRoute] int idTest, CancellationToken ct)
    {
        var query = new GetTestQuery(User.GetId()!.Value, idTest);
        var result = await _mediator.Send(query, ct);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Marks test as finished
    /// </summary>
    [HttpPut("{idTest:int}/finish")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> FinishTest([FromRoute] int idTest, CancellationToken ct)
    {
        var query = new FinishTestCommand(User.GetId()!.Value, idTest);
        var result = await _mediator.Send(query, ct);
        return !result ? Conflict() : NoContent();
    }

    /// <summary>
    /// Gets user tests
    /// </summary>
    [HttpGet("user")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserTestsResponseDTO))]
    public async Task<IActionResult> GetUserTests(CancellationToken ct)
    { 
        var query = new GetUserTestsQuery(User.GetId()!.Value);
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }
}