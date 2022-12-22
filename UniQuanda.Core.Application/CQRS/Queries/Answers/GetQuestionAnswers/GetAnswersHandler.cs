using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Answers.GetQuestionAnswers;

public class GetAnswersHandler : IRequestHandler<GetQuestionAnswersQuery, GetQuestionAnswersResponseDTO>
{
    private readonly IAnswerRepository _answerRepository;

    public GetAnswersHandler(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<GetQuestionAnswersResponseDTO> Handle(GetQuestionAnswersQuery request, CancellationToken ct)
    {
        if (request.IdAnswer != null)
            request.Page = await _answerRepository.GetAnswerPageAsync(request.IdQuestion, request.IdAnswer.Value, ct);

        return new GetQuestionAnswersResponseDTO
        {
            Page = request.Page.Value,
            Answers = await _answerRepository.GetQuestionAnswersAsync(request.IdQuestion, request.Page.Value, request.IdComment, request.IdLoggedUser, ct)
        };
    }
}
