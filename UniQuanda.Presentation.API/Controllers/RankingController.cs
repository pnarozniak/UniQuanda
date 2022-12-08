using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Queries.Auth.GetReportTypes;
using UniQuanda.Core.Application.CQRS.Queries.Ranking.GetTop5Users;

namespace UniQuanda.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RankingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets 5 users with highest amount of points
        /// </summary>
        [HttpGet("top-5-users")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTop5UsersResponseDTO))]
        public async Task<IActionResult> GetTop5Users(CancellationToken ct)
        {
            var query = new GetTop5UsersQuery();
            var response = await this._mediator.Send(query, ct);
            return Ok(response);
        }
    }
}
