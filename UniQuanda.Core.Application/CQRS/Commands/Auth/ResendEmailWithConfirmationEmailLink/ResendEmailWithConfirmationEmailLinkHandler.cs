using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResendEmailWithConfirmationEmailLink;

public class ResendEmailWithConfirmationEmailLinkHandler : IRequestHandler<ResendEmailWithConfirmationEmailLinkCommand, bool>
{
    private readonly IAuthRepository _authRepository;
    private readonly IEmailService _emailService;
    private readonly ITokensService _tokensService;
    private readonly IExpirationService _expirationService;

    public ResendEmailWithConfirmationEmailLinkHandler(
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

    public async Task<bool> Handle(ResendEmailWithConfirmationEmailLinkCommand request, CancellationToken ct)
    {
        var user = await _authRepository.GetUserWithEmailsByIdAsync(request.IdUser, ct);
        if (user is null)
            return false;

        var (idEmail, actionType) = await _authRepository.GetEmailToConfirmAsync(request.IdUser, ct);
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
        if (!isUpdateSuccessful) return false;

        var recipient = user.Emails.SingleOrDefault(u => u.Id == idEmail).Value;
        if (actionType == UserActionToConfirmEnum.NewExtraEmail)
        {
            await _emailService.SendConfirmationEmailToAddNewExtraEmailAsync(
                recipient, user.Nickname, userEmailToConfirm.ConfirmationToken, request.UserAgentInfo);
        }
        else
        {
            await _emailService.SendConfirmationEmailToUpdateMainEmailAsync(
                recipient, user.Nickname, userEmailToConfirm.ConfirmationToken, request.UserAgentInfo);
        }

        return true;
    }
}