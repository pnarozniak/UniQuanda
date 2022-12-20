using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Queries.Permission.AskQuestionPermission;
using UniQuanda.Core.Application.Shared.Models;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Presentation.API.Extensions;

namespace UniQuanda.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimitController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LimitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets user limits to ask question
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LimitCheckResponseDTO))]
        [Authorize(Roles = AppRole.User)]
        [HttpGet("question-add")]
        public async Task<IActionResult> GetAskQuestionPermissionsUsages(CancellationToken ct)
        {
            var query = new AskQuestionPermission(User.GetId()??0);
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }
    }
}
