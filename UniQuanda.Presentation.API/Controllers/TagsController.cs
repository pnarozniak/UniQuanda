using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.CQRS.Queries.Tags.GetTags;
using UniQuanda.Core.Domain.Enums;

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
        /// <param name="addCount">If true, response will contain all records in given filters</param>
        /// <param name="order">Order direction of result</param>
        /// <param name="idTag">Parent tag Id</param>
        /// <param name="keyword">Text to search</param>
        /// <param name="addParentTagData">If true and idTag is provided, response will contain parent tag description and name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Max. pageSize tags that mached conditions</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTagsResponseDTO))]
        [HttpGet]
        public async Task<IActionResult> GetTags(
            [FromQuery][Required] int page,
            [FromQuery][Required] int pageSize,
            [FromQuery][Required] bool addCount,
            [FromQuery][Required] OrderDirectionEnum order,
            [FromQuery] int? idTag,
            [FromQuery] string? keyword,
            [FromQuery] bool? addParentTagData,
            CancellationToken ct)
        {
            var dto = new GetTagsRequestDTO
            {
                Page = page,
                PageSize = pageSize,
                TagId = idTag,
                Keyword = keyword,
                AddCount = addCount,
                AddParentTagData = addParentTagData,
                OrderDirection = order
            };
            var query = new GetTagsQuery(dto);
            return Ok(await _mediator.Send(query, ct));
        }
    }
}
