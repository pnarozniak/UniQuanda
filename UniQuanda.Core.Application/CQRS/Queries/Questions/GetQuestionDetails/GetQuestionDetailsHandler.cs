using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestionDetails;

internal class GetQuestionDetailsHandler : IRequestHandler<GetQuestionDetailsQuery, GetQuestionDetailsResponseDTO?>
{
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionDetailsHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<GetQuestionDetailsResponseDTO?> Handle(GetQuestionDetailsQuery request, CancellationToken ct)
    {
        var question = await _questionRepository.GetQuestionDetailsAsync(request.IdQuestion, request.IdLoggedUser, ct);
        if (question is null)
            return null;

        if (request.IdLoggedUser != null)
            question.IsQuestionFollowed = await _questionRepository.IsQuestionFollowedByUserAsync(request.IdQuestion, request.IdLoggedUser.Value, ct);

        return new()
        {
            QuestionDetails = question
        };
    }
}