using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.UpdateAnswerLikeValue;

public class UpdateAnswerLikeValueHandler : IRequestHandler<UpdateAnswerLikeValueCommand, UpdateAnswerLikeValueResponseDTO>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IAppUserRepository _appUserRepository;

    public UpdateAnswerLikeValueHandler(IAnswerRepository answerRepository, IAppUserRepository appUserRepository)
    {
        _answerRepository = answerRepository;
        _appUserRepository = appUserRepository;
    }

    public async Task<UpdateAnswerLikeValueResponseDTO> Handle(UpdateAnswerLikeValueCommand request, CancellationToken ct)
    {
        var repoResult = await _answerRepository.UpdateAnswerLikeValueAsync(request.IdAnswer, request.LikeValue, request.IdLoggedUser, ct);

        if (repoResult.IsUpdateSuccessful == true)
            await _appUserRepository.UpdateAppUserPointsForLikeValueInTagsAsync(request.IdAnswer, repoResult.LikesIncreasedBy, ct);

        return new UpdateAnswerLikeValueResponseDTO()
        {
            IsUpdateSuccessful = repoResult.IsUpdateSuccessful,
            LikeValue = repoResult.LikeValue,
            Likes = repoResult.Likes
        };
    }
}