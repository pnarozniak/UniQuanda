using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailAndNicknameAvailable;
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
        ///    Returns tags with filtering
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Amount of tags on page</param>
        /// <param name="idTag">Parent tag Id</param>
        /// <param name="keyword">Text to search</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Max. pageSize tags that mached conditions. When page is = 1, response will contain total tags count </returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTagsResponseDTO))]
        [HttpGet]
        public async Task<IActionResult> GetTags(
            [FromQuery][Required] int page,
            [FromQuery][Required] int pageSize,
            [FromQuery] int? idTag,
            [FromQuery] string? keyword,
            CancellationToken ct)
        {
            var dto = new GetTagsRequestDTO
            {
                Page = page,
                PageSize = pageSize,
                TagId = idTag,
                Keyword = keyword
            };
            var query = new GetTagsQuery(dto);
            return Ok(await _mediator.Send(query, ct));
        }
    }
}
