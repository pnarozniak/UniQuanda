using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Answers.AddAnswer;
using UniQuanda.Core.Application.CQRS.Commands.Answers.DeleteAnswer;
using UniQuanda.Core.Application.CQRS.Commands.Answers.MarkAnswerAsCorrect;
using UniQuanda.Core.Application.CQRS.Commands.Answers.UpdateAnswer;
using UniQuanda.Core.Application.CQRS.Commands.Answers.UpdateAnswerLikeValue;
using UniQuanda.Core.Application.CQRS.Queries.Answers.GetAllComments;
using UniQuanda.Core.Application.CQRS.Queries.Answers.GetQuestionAnswers;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Presentation.API.Attributes;
using UniQuanda.Presentation.API.Extensions;

namespace UniQuanda.Presentation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnswersController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnswersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Recaptcha]
    [Authorize(Roles = AppRole.User)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddAnswerResponseDTO))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddAnswer([FromBody] AddAnswerRequestDTO request, CancellationToken ct)
    {
        var command = new AddAnswerCommand(request, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result.Status ? Ok(result) : Conflict();
    }

    [HttpPut]
    [Recaptcha]
    [Authorize(Roles = AppRole.User)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAnswer([FromBody] UpdateAnswerRequestDTO request, CancellationToken ct)
    {
        var command = new UpdateAnswerCommand(request, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result switch
        {
            true => NoContent(),
            false => Conflict(),
            null => NotFound()
        };
    }

    [HttpGet("question/{idQuestion}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetQuestionAnswersResponseDTO))]
    public async Task<IActionResult> GetQuestionAnswers([FromRoute] int idQuestion, [FromQuery] int? page, [FromQuery] int? idAnswer, [FromQuery] int? idComment, CancellationToken ct)
    {
        var query = new GetQuestionAnswersQuery(idQuestion, page, idAnswer, idComment, User.GetId());
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    [HttpGet("comments/{idAnswerParent}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllCommentsResponseDTO))]
    public async Task<IActionResult> GetAllComments([FromRoute] int idAnswerParent, CancellationToken ct)
    {
        var query = new GetAllCommentsQuery(idAnswerParent, User.GetId());
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }


    [HttpPut("correct/{idAnswer}")]
    [Authorize(Roles = AppRole.User)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> MarkAnswerAsCorrect([FromRoute] int idAnswer, CancellationToken ct)
    {
        var command = new MarkAnswerAsCorrectCommand(idAnswer, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result switch
        {
            true => NoContent(),
            false => Conflict(),
            null => NotFound()
        };
    }


    [HttpPut("like-value")]
    [Authorize(Roles = AppRole.User)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAnswerLikeValue([FromBody] UpdateAnswerLikeValueRequestDTO request, CancellationToken ct)
    {
        var command = new UpdateAnswerLikeValueCommand(request, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result.IsUpdateSuccessful switch
        {
            true => Ok(result),
            false => Conflict(),
            null => NotFound()
        };
    }

    [HttpDelete("{idAnswer}")]
    [Authorize(Roles = AppRole.User)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteAnswer([FromRoute] int idAnswer, CancellationToken ct)
    {
        var command = new DeleteAnswerCommand(idAnswer, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result switch
        {
            true => NoContent(),
            false => Conflict(),
            null => NotFound()
        };
    }
}
