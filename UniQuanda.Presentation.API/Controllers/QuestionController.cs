using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Application.CQRS.Commands.Questions.AddQuestion;
using UniQuanda.Core.Application.CQRS.Commands.Questions.DeleteQuestion;
using UniQuanda.Core.Application.CQRS.Commands.Questions.FollowQuestion;
using UniQuanda.Core.Application.CQRS.Commands.Questions.UpdateQuestion;
using UniQuanda.Core.Application.CQRS.Commands.Questions.UpdateQuestionView;
using UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestionDetails;
using UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestionDetailsForUpdate;
using UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestions;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Infrastructure.Enums;
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
        [Authorize(Roles = JwtTokenRole.User)]
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

        [HttpGet("{idQuestion}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetQuestionDetailsResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuestionDetails([FromRoute] int idQuestion, CancellationToken ct)
        {
            var query = new GetQuestionDetailsQuery(idQuestion, User.GetId());
            var result = await this._mediator.Send(query, ct);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut("follow/{idQuestion}")]
        [Authorize(Roles = JwtTokenRole.User)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> FollowQuestion([FromRoute] int idQuestion, CancellationToken ct)
        {
            var command = new FollowQuestionCommand(idQuestion, User.GetId()!.Value);
            var result = await this._mediator.Send(command, ct);
            return result ? NoContent() : Conflict();
        }

        [HttpDelete("{idQuestion}")]
        [Authorize(Roles = JwtTokenRole.User)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int idQuestion, CancellationToken ct)
        {
            var command = new DeleteQuestionCommand(idQuestion, User.GetId()!.Value);
            var result = await this._mediator.Send(command, ct);
            return result.Status switch
            {
                DeleteQuestionResultEnum.Successful => NoContent(),
                DeleteQuestionResultEnum.ContentNotExist => NotFound(),
                _ => Conflict(result)
            };

        }

        [HttpPut("views/{idQuestion}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateViews([FromRoute] int idQuestion, CancellationToken ct)
        {
            //var clientIp = User.GetId() != null ? HttpContext.Connection.RemoteIpAddress?.ToString() : null;
            var command = new UpdateQuestionViewCommand(idQuestion, User.GetId());
            await this._mediator.Send(command, ct);
            return NoContent();
        }

        [HttpGet("update/{idQuestion}")]
        [Authorize(Roles = JwtTokenRole.User)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetQuestionDetailsForUpdateResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuestionDetailsForUpdate([FromRoute] int idQuestion, CancellationToken ct)
        {
            var query = new GetQuestionDetailsForUpdateQuery(idQuestion, User.GetId()!.Value);
            var result = await this._mediator.Send(query, ct);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = JwtTokenRole.User)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionRequestDTO request, CancellationToken ct)
        {
            var command = new UpdateQuestionCommand(request, User.GetId()!.Value);
            var result = await this._mediator.Send(command, ct);
            return result switch
            {
                true => NoContent(),
                null => NotFound(),
                false => Conflict()
            };
        }
    }
}