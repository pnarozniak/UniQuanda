using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.GetUserInfo;

public class GetUserInfoHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoResponseDTO?>
{
    private readonly IAuthRepository _authRepository;
    private readonly IAppUserRepository _appUserRepository;

    public GetUserInfoHandler(IAuthRepository authRepository, IAppUserRepository appUserRepository)
    {
        _authRepository = authRepository;
        _appUserRepository = appUserRepository;
    }

    public async Task<GetUserInfoResponseDTO?> Handle(GetUserInfoQuery request, CancellationToken ct)
    {
        var dbUser = await _authRepository.GetUserByIdAsync(request.IdUser, ct);
        if (dbUser is null) return null;

        return new GetUserInfoResponseDTO
        {
            RefreshToken = dbUser.RefreshToken!,
            Nickname = dbUser.Nickname,
            Avatar = await _appUserRepository.GetUserAvatarAsync(dbUser.Id, ct)
        };
    }
}