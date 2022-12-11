using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Queries.Search.Search;

namespace UniQuanda.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class SearchController : ControllerBase
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResponseDTO))]
    public async Task<IActionResult> Search(
        [FromQuery] SearchRequestDTO request,
        CancellationToken ct)
    {
        var query = new SearchQuery(request);
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

}