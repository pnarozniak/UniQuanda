using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Application.CQRS.Commands.Questions.AddQuestion;
using UniQuanda.Core.Application.CQRS.Queries.Questions;
using UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestions;
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
        [Authorize(Roles = "user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddQuestionResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AddQuestionResponseDTO))]
        public async Task<IActionResult> AddContent([FromBody] AddQuestionRequestDTO request, CancellationToken ct)
        {
            var command = new AddQuestionCommand(request, User.GetId()!.Value);
            var result = await this._mediator.Send(command, ct);
            return result.QuestionId != null ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        ///     Gets questions from the database using filters
        /// </summary>
        /// <param name="request"></param>
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