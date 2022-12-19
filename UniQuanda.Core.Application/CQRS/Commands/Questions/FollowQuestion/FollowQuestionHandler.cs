using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Questions.FollowQuestion;

public class FollowQuestionHandler : IRequestHandler<FollowQuestionCommand, bool>
{
    private readonly IQuestionRepository _questionRepository;

    public FollowQuestionHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<bool> Handle(FollowQuestionCommand request, CancellationToken ct)
    {
        return await _questionRepository.UpdateQuestionFollowStatusAsync(request.IdQuestion, request.IdLoggedUser, ct);
    }
}