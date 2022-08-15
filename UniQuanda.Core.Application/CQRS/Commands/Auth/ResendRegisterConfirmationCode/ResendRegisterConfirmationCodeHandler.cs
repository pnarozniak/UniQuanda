using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResendRegisterConfirmationCode;

public class ResendRegisterConfirmationCodeHandler : IRequestHandler<ResendRegisterConfirmationCodeCommand, bool>
{
    private readonly IAuthRepository _authRepository;
    private readonly IEmailService _emailService;
    private readonly ITokensService _tokensService;

    public ResendRegisterConfirmationCodeHandler(
        IAuthRepository authRepository,
        IEmailService emailService,
        ITokensService tokensService)
    {
        _authRepository = authRepository;
        _emailService = emailService;
        _tokensService = tokensService;
    }

    public async Task<bool> Handle(ResendRegisterConfirmationCodeCommand command, CancellationToken ct)
    {
        var confirmationCode = _tokensService.GenerateEmailConfirmationToken();
        var isUpdated =
            await _authRepository.UpdateTempUserEmailConfirmationCodeAsync(command.Email, confirmationCode, ct);
        if (isUpdated is null or false) return false;

        await _emailService.SendRegisterConfirmationEmailAsync(command.Email, confirmationCode);
        return true;
    }
}