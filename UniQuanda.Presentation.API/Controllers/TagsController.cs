using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Queries.Tags.GetTags;

namespace UniQuanda.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TagsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        ///    Returns in a given order tags, using filtering and paging
        /// </summary>
        /// <param name="request">Request object</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Max. pageSize tags that mached conditions</returns>
        /// <response code="200">When managed to get tags</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTagsResponseDTO))]
        [HttpGet]
        public async Task<IActionResult> GetTags(
            [FromQuery]GetTagsRequestDTO request,
            CancellationToken ct)
        {
            var query = new GetTagsQuery(request);
            return Ok(await _mediator.Send(query, ct));
        }
    }
}
