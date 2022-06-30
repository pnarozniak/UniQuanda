using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IAuthRepository _authRepository;
    private readonly IEmailService _emailService;
    private readonly IExpirationService _expirationService;
    private readonly IPasswordsService _passwordsService;
    private readonly ITokensService _tokensService;

    public RegisterHandler(
        IAuthRepository authRepository,
        ITokensService tokensService,
        IEmailService emailService,
        IPasswordsService passwordsService,
        IExpirationService expirationService)
    {
        _authRepository = authRepository;
        _tokensService = tokensService;
        _emailService = emailService;
        _passwordsService = passwordsService;
        _expirationService = expirationService;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var confirmationToken = _tokensService.GenerateEmailConfirmationToken();

        request.NewUser.EmailConfirmationToken = confirmationToken;
        request.NewUser.HashedPassword = _passwordsService.HashPassword(request.PlainPassword);
        request.NewUser.ExistsUntil = DateTime.UtcNow.AddHours(_expirationService.GetNewUserExpirationInHours());

        var isRegistered = await _authRepository.RegisterNewUserAsync(request.NewUser);
        if (!isRegistered)
            return false;

        await _emailService.SendRegisterConfirmationEmailAsync(request.NewUser.Email, confirmationToken);
        return true;
    }
}