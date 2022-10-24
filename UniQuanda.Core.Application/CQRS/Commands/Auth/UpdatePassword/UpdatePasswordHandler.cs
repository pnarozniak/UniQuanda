using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordCommand, UpdateResultOfEmailOrPasswordEnum>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;

    public UpdatePasswordHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
    }

    public async Task<UpdateResultOfEmailOrPasswordEnum> Handle(UpdatePasswordCommand request, CancellationToken ct)
    {
        var hashedPassword = await _authRepository.GetUserHashedPasswordByIdAsync(request.IdUser, ct);
        if (hashedPassword == null)
            return UpdateResultOfEmailOrPasswordEnum.UserNotExist;

        if (!_passwordsService.VerifyPassword(request.OldPassword, hashedPassword))
            return UpdateResultOfEmailOrPasswordEnum.InvalidPassword;

        request.NewPassword = _passwordsService.HashPassword(request.NewPassword);

        var updateResult = await _authRepository.UpdateUserPasswordAsync(request.IdUser, request.NewPassword, ct);
        return updateResult switch
        {
            null => UpdateResultOfEmailOrPasswordEnum.UserNotExist,
            false => UpdateResultOfEmailOrPasswordEnum.NotSuccessful,
            true => UpdateResultOfEmailOrPasswordEnum.Successful
        };
    }
}