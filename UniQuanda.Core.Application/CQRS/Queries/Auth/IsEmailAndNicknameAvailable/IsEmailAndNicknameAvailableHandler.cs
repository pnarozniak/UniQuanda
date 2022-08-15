using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailAndNicknameAvailable;

public class
    IsEmailAndNicknameAvailableHandler : IRequestHandler<IsEmailAndNicknameAvailableQuery,
        IsEmailAndNicknameAvailableResponseDTO>
{
    private readonly IAuthRepository _authRepository;

    public IsEmailAndNicknameAvailableHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IsEmailAndNicknameAvailableResponseDTO> Handle(IsEmailAndNicknameAvailableQuery request,
        CancellationToken ct)
    {
        return new IsEmailAndNicknameAvailableResponseDTO
        {
            IsEmailAvailable = !await _authRepository.IsEmailUsedAsync(request.Email, ct),
            IsNicknameAvailable = !await _authRepository.IsNicknameUsedAsync(request.Nickname, ct)
        };
    }
}