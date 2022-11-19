using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmail;

public class AddExtraEmailHandler : IRequestHandler<AddExtraEmailCommand, AddExtraEmailResponseDTO>
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

    public async Task<AddExtraEmailResponseDTO> Handle(AddExtraEmailCommand request, CancellationToken ct)
    {
        var user = await _authRepository.GetUserWithEmailsByIdAsync(request.IdUser, ct);
        if (user is null)
            return new AddExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.ContentNotExist };

        if (!_passwordsService.VerifyPassword(request.PlainPassword, user.HashedPassword))
            return new AddExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.InvalidPassword };

        var isEmailAvailable = await _authRepository.IsEmailAvailableAsync(null, request.NewExtraEmail, ct);
        if (!isEmailAvailable)
            return new AddExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.EmailNotAvailable };

        var isUserAllowed = await _authRepository.IsUserAllowedToAddExtraEmailAsync(request.IdUser, ct);
        if (isUserAllowed == CheckOptionOfAddNewExtraEmail.UserNotExist)
            return new AddExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.ContentNotExist };
        else if (isUserAllowed == CheckOptionOfAddNewExtraEmail.OverLimitOfExtraEmails)
            return new AddExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.OverLimitOfExtraEmails };
        else if (isUserAllowed == CheckOptionOfAddNewExtraEmail.UserHasActionToConfirm)
            return new AddExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.UserHasActionToConfirm };

        var userEmailToConfirm = new UserEmailToConfirm
        {
            IdUser = request.IdUser,
            Email = request.NewExtraEmail,
            ConfirmationToken = _tokensService.GenerateNewEmailConfirmationToken(),
            ExistsUntil = DateTime.UtcNow.AddHours(_expirationService.GetEmailConfirmationExpirationInHours())
        };

        var addResult = await _authRepository.AddExtraEmailAsync(userEmailToConfirm, ct);

        if (addResult == true)
            await _emailService.SendEmailWithEmailConfirmationLinkAsync(userEmailToConfirm.Email, userEmailToConfirm.ConfirmationToken);

        return addResult switch
        {
            null => new AddExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.ContentNotExist},
            false => new AddExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.UnSuccessful},
            true => new AddExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.Successful}
        };
    }
}