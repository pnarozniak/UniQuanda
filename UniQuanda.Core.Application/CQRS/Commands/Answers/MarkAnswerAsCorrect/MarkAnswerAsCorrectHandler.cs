using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.MarkAnswerAsCorrect;

public class MarkAnswerAsCorrectHandler : IRequestHandler<MarkAnswerAsCorrectCommand, bool?>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IAppUserRepository _appUserRepository;

    public MarkAnswerAsCorrectHandler(IAnswerRepository answerRepository, IAppUserRepository appUserRepository)
    {
        _answerRepository = answerRepository;
        _appUserRepository = appUserRepository;
    }

    public async Task<bool?> Handle(MarkAnswerAsCorrectCommand request, CancellationToken ct)
    {
        var (isSuccess, idAuthorPrevCorrectAnswer) = await _answerRepository.UpdateAnswerCorrectnessAsync(request.IdAnswer, request.IdLoggedUser, ct);
        if (isSuccess == true)
            _ = Task.Run(async () => await _appUserRepository.UpdateAppUserPointsForCorrectAnswerInTagsAsync(request.IdAnswer, idAuthorPrevCorrectAnswer, ct));

        return isSuccess;
    }
}