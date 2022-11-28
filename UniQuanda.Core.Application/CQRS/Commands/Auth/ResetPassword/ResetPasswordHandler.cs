using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResetPasword;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly IAuthRepository _authRepository;
    private readonly IEmailService _emailService;
    private readonly IPasswordsService _passwordService;

    public ResetPasswordHandler(
        IAuthRepository authRepository,
        IEmailService emailService,
        IPasswordsService passwordsService)
    {
        _authRepository = authRepository;
        _emailService = emailService;
        _passwordService = passwordsService;
    }

    public async Task<bool> Handle(ResetPasswordCommand command, CancellationToken ct)
    {
        var dbUser = await _authRepository.GetUserByEmailAsync(command.Email, ct);
        if (dbUser is null || !dbUser.IsEmailConfirmed || dbUser.IsOAuthUser) return false;

        var action = await _authRepository.GetUserActionToConfirmAsync(UserActionToConfirmEnum.RecoverPassword,
            command.RecoveryToken, ct);

        if (action is null || action.IdUser != dbUser.Id || action.ExistsUntil <= DateTime.UtcNow) return false;

        var hashedPassword = _passwordService.HashPassword(command.NewPassword);
        var isReseted = await _authRepository.ResetUserPasswordAsync(dbUser.Id, action.Id, hashedPassword, ct);
        if (!isReseted) return false;

        await _emailService.SendEmailAboutUpdatedPasswordAsync(command.Email, dbUser.Nickname, command.UserAgentInfo);
        return true;
    }
}