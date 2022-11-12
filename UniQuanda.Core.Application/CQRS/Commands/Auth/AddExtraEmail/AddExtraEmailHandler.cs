using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmail;

public class AddExtraEmailHandler : IRequestHandler<AddExtraEmailCommand, UpdateSecurityResultEnum>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;
    private readonly IEmailService _emailService;
    private readonly ITokensService _tokensService;
    private readonly IExpirationService _expirationService;

    public AddExtraEmailHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService,
        IEmailService emailService,
        ITokensService tokensService,
        IExpirationService expirationService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
        _emailService = emailService;
        _tokensService = tokensService;
        _expirationService = expirationService;
    }

    public async Task<UpdateSecurityResultEnum> Handle(AddExtraEmailCommand request, CancellationToken ct)
    {
        var user = await _authRepository.GetUserWithEmailsByIdAsync(request.IdUser, ct);
        if (user is null)
            return UpdateSecurityResultEnum.ContentNotExist;

        if (!_passwordsService.VerifyPassword(request.PlainPassword, user.HashedPassword))
            return UpdateSecurityResultEnum.InvalidPassword;

        var isEmailAvailable = await _authRepository.IsEmailAvailableAsync(null, request.NewExtraEmail, ct);
        if (!isEmailAvailable)
            return UpdateSecurityResultEnum.EmailNotAvailable;

        var isUserAllowed = await _authRepository.IsUserAllowedToAddExtraEmailAsync(request.IdUser, ct);
        if (isUserAllowed == AddExtraEmailStatus.UserNotExist)
            return UpdateSecurityResultEnum.ContentNotExist;
        else if (isUserAllowed == AddExtraEmailStatus.OverLimitOfExtraEmails)
            return UpdateSecurityResultEnum.OverLimitOfExtraEmails;
        else if (isUserAllowed == AddExtraEmailStatus.UserHasActionToConfirm)
            return UpdateSecurityResultEnum.UserHasActionToConfirm;

        var userEmailToConfirm = new UserEmailToConfirm
        {
            IdUser = request.IdUser,
            Email = request.NewExtraEmail,
            ConfirmationToken = _tokensService.GenerateNewEmailConfirmationToken(),
            ExistsUntil = DateTime.UtcNow.AddHours(_expirationService.GetNewUserExpirationInHours()),
        };

        var addResult = await _authRepository.AddExtraEmailAsync(userEmailToConfirm, ct);

        if (addResult == true)
            await _emailService.SendInformationToConfirmEmail(userEmailToConfirm.Email, userEmailToConfirm.ConfirmationToken);

        return addResult switch
        {
            null => UpdateSecurityResultEnum.ContentNotExist,
            false => UpdateSecurityResultEnum.DbConflict,
            true => UpdateSecurityResultEnum.Successful
        };
    }
}