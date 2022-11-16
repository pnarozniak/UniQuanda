using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResendConfirmationEmail;

public class ResendConfirmationEmailHandler : IRequestHandler<ResendConfirmationEmailCommand, bool>
{
    private readonly IAuthRepository _authRepository;
    private readonly IEmailService _emailService;
    private readonly ITokensService _tokensService;
    private readonly IExpirationService _expirationService;

    public ResendConfirmationEmailHandler(
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

    public async Task<bool> Handle(ResendConfirmationEmailCommand request, CancellationToken ct)
    {
        var user = await _authRepository.GetUserWithEmailsByIdAsync(request.IdUser, ct);
        if (user is null)
            return false;

        var idEmail = await _authRepository.GetIdEmailToConfirmAsync(request.IdUser, ct);
        if (idEmail is null)
            return false;

        var userEmailToConfirm = new UserEmailToConfirm
        {
            IdUser = request.IdUser,
            IdEmail = idEmail,
            ConfirmationToken = _tokensService.GenerateNewEmailConfirmationToken(),
            ExistsUntil = DateTime.UtcNow.AddHours(_expirationService.GetEmailConfirmationExpirationInHours()),
        };

        var isUpdateSuccessful = await _authRepository.UpdateActionToConfirmEmailAsync(userEmailToConfirm, ct);
        if (isUpdateSuccessful)
            await _emailService.SendInformationToConfirmEmail(user.Emails.SingleOrDefault(u => u.Id == idEmail).Value, userEmailToConfirm.ConfirmationToken);

        return isUpdateSuccessful;
    }
}