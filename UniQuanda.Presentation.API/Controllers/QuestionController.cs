using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Question.AddQuestion;
using UniQuanda.Core.Application.CQRS.Queries.Question.GetQuestionByIdAndTitle;
using UniQuanda.Core.Application.CQRS.Queries.Question.GetQuestions;

namespace UniQuanda.Presentation.API.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuestionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestions()
    {
        return Ok(await _mediator.Send(new GetQuestionsQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> PostQuestions([FromBody] AddQuestionRequestDTO request)
    {
        return Ok(await _mediator.Send(new AddQuestionCommand(request)));
    }

    [HttpGet("{id:int}/{title}")]
    public async Task<IActionResult> GetQuestionByIdAndTitle(int id, string title)
    {
        return Ok(await _mediator
            .Send(
                new GetQuestionByIdAndTitleQuery(
                    new GetQuestionByIdAndTitleRequestDTO
                    {
                        QuestionId = id,
                        Title = title
                    }
                )
            )
        );
    }
}