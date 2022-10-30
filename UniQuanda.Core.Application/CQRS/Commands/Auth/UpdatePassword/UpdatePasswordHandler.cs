using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordCommand, UpdateSecurityResultEnum>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;
    private readonly IEmailService _emailService;

    public UpdatePasswordHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService,
        IEmailService emailService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
        _emailService = emailService;
    }

    public async Task<UpdateSecurityResultEnum> Handle(UpdatePasswordCommand request, CancellationToken ct)
    {
        if (request.NewPassword == request.OldPassword)
            return UpdateSecurityResultEnum.Successful;

        var user = await _authRepository.GetUserByIdAsync(request.IdUser, ct);
        if (user is null)
            return UpdateSecurityResultEnum.ContentNotExist;

        if (!_passwordsService.VerifyPassword(request.OldPassword, user.HashedPassword))
            return UpdateSecurityResultEnum.InvalidPassword;

        request.NewPassword = _passwordsService.HashPassword(request.NewPassword);

        var updateResult = await _authRepository.UpdateUserPasswordAsync(request.IdUser, request.NewPassword, ct);
        if (updateResult == true)
            await _emailService.SendInformationAboutUpdatePasswordAsync(user.Emails.SingleOrDefault(e => e.IsMain).Value, user.Nickname);

        return updateResult switch
        {
            null => UpdateSecurityResultEnum.ContentNotExist,
            false => UpdateSecurityResultEnum.DbConflict,
            true => UpdateSecurityResultEnum.Successful
        };
    }
}