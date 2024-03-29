using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.RecoverPassword;

public class RecoverPasswordHandler : IRequestHandler<RecoverPasswordCommand, bool>
{
    private readonly IAuthRepository _authRepository;
    private readonly IEmailService _emailService;
    private readonly IExpirationService _expirationService;
    private readonly ITokensService _tokensService;

    public RecoverPasswordHandler(
        IAuthRepository authRepository,
        IEmailService emailService,
        ITokensService tokensService,
        IExpirationService expirationService)
    {
        _authRepository = authRepository;
        _emailService = emailService;
        _tokensService = tokensService;
        _expirationService = expirationService;
    }

    public async Task<bool> Handle(RecoverPasswordCommand command, CancellationToken ct)
    {
        var dbUser = await _authRepository.GetUserByEmailAsync(command.Email, ct);
        if (dbUser is null || !dbUser.IsEmailConfirmed || dbUser.IsOAuthUser) return false;

        var recoveryToken = _tokensService.GeneratePasswordRecoveryToken();
        var actionExp = DateTime.UtcNow.AddMinutes(_expirationService.GetRecoverPasswordActionExpirationInMinutes());
        var isActionCreated = await _authRepository.CreateUserActionToConfirmAsync(dbUser.Id,
            UserActionToConfirmEnum.RecoverPassword, recoveryToken, actionExp, null, ct);
        if (isActionCreated is null or false) return false;

        await _emailService.SendPasswordRecoveryEmailAsync(command.Email, recoveryToken, command.UserAgentInfo);
        return true;
    }
}