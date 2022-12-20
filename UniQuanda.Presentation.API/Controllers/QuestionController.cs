using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Application.CQRS.Commands.Questions.AddQuestion;
using UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestions;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Presentation.API.Extensions;

namespace UniQuanda.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Adds a question to the database
        /// </summary>
        /// <param name="request">Question data</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id of added question if success, status info otherwise</returns>
        [HttpPost]
        [Authorize(Roles = AppRole.User)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddContent([FromBody] AddQuestionRequestDTO request, CancellationToken ct)
        {
            var command = new AddQuestionCommand(request, User.GetId()!.Value);
            var result = await this._mediator.Send(command, ct);
            if (result.Status == AskQuestionResultEnum.QuestionAsked)
                return Ok(result.QuestionId);
            if (result.Status == AskQuestionResultEnum.PermissionDenied) return Unauthorized();
            if (result.Status == AskQuestionResultEnum.LimitsExceeded) return Forbid();
            if (result.Status == AskQuestionResultEnum.TagsNotFound) return BadRequest("Tags not found");
            return Conflict();

        }

        /// <summary>
        ///     Gets questions from the database using filters
        /// </summary>
        /// <param name="request">Request body</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetQuestionsResponseDTO))]
        public async Task<IActionResult> GetQuestions([FromQuery] GetQuestionsRequestDTO request, CancellationToken ct)
        {
            var query = new GetQuestionsQuery(request);
            var result = await this._mediator.Send(query, ct);
            return Ok(result);
        }
    }
}