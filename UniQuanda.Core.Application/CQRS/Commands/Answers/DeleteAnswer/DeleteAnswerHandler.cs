using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.DeleteAnswer;

public class DeleteAnswerHandler : IRequestHandler<DeleteAnswerCommand, bool?>
{
    private readonly IAnswerRepository _answerRepository;

    public DeleteAnswerHandler(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<bool?> Handle(DeleteAnswerCommand request, CancellationToken ct)
    {
        return await _answerRepository.DeleteAnswerAsync(request.IdAnswer, request.IdLoggedUser, ct);
    }
}