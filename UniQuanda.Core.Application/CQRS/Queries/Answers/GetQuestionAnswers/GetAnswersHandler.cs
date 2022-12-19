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
        return new GetQuestionAnswersResponseDTO
        {
            Answers = await _answerRepository.GetQuestionAnswersAsync(request.IdQuestion, request.Page, request.IdLoggedUser, ct)
        };
    }
}
