using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateMainEmail;

public class UpdateMainEmailHandler : IRequestHandler<UpdateMainEmailCommand, UpdateSecurityResultEnum>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;
    private readonly IEmailService _emailService;
    private readonly ITokensService _tokensService;
    private readonly IExpirationService _expirationService;

    public UpdateMainEmailHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService,
        IEmailService emailService,
        ITokensService tokensService,
        IExpirationService expirationService)
    {
        this._authRepository = authRepository;
        this._passwordsService = passwordsService;
        this._emailService = emailService;
        this._tokensService = tokensService;
        this._expirationService = expirationService;
    }

    public async Task<UpdateSecurityResultEnum> Handle(UpdateMainEmailCommand request, CancellationToken ct)
    {
        var user = await _authRepository.GetUserWithEmailsByIdAsync(request.IdUser, ct);
        if (user is null)
            return UpdateSecurityResultEnum.ContentNotExist;

        if (!_passwordsService.VerifyPassword(request.PlainPassword, user.HashedPassword))
            return UpdateSecurityResultEnum.InvalidPassword;

        bool? updateResult;
        if (request.IdExtraEmail != null)
        {
            var userEmailToConfirm = new UserEmailToConfirm
            {
                IdUser = request.IdUser,
                IdEmail = request.IdExtraEmail.Value,
                ConfirmationToken = _tokensService.GenerateNewEmailConfirmationToken(),
                ExistsUntil = DateTime.UtcNow.AddHours(_expirationService.GetEmailConfirmationExpirationInHours()),
            };
            updateResult = await _authRepository.UpdateUserMainEmailByExtraEmailAsync(userEmailToConfirm, ct);
        }
        else
        {
            var isEmailAvailable = await _authRepository.IsEmailAvailableAsync(request.IdUser, request.NewMainEmail, ct);
            if (!isEmailAvailable)
                return UpdateSecurityResultEnum.EmailNotAvailable;

            var getResult = await _authRepository.GetExtraEmailIdAsync(request.IdUser, request.NewMainEmail, ct);
            if (getResult.isConnected && getResult.idEmail != null)
            {
                var userEmailToConfirm = new UserEmailToConfirm
                {
                    IdUser = request.IdUser,
                    IdEmail = getResult.idEmail.Value,
                    ConfirmationToken = _tokensService.GenerateNewEmailConfirmationToken(),
                    ExistsUntil = DateTime.UtcNow.AddHours(_expirationService.GetEmailConfirmationExpirationInHours()),
                };
                updateResult = await _authRepository.UpdateUserMainEmailByExtraEmailAsync(userEmailToConfirm, ct);
            }
            else if (getResult.isConnected && getResult.idEmail == null)
            {
                updateResult = true;
            }
            else
            {
                var userEmailToConfirm = new UserEmailToConfirm
                {
                    IdUser = request.IdUser,
                    Email = request.NewMainEmail,
                    ConfirmationToken = _tokensService.GenerateNewEmailConfirmationToken(),
                    ExistsUntil = DateTime.UtcNow.AddHours(_expirationService.GetEmailConfirmationExpirationInHours()),
                };
                updateResult = await _authRepository.UpdateUserMainEmailAsync(userEmailToConfirm, ct);

                if (updateResult == true)
                    await _emailService.SendInformationToConfirmEmail(userEmailToConfirm.Email, userEmailToConfirm.ConfirmationToken);
            }
        }

        return updateResult switch
        {
            null => UpdateSecurityResultEnum.ContentNotExist,
            false => UpdateSecurityResultEnum.DbConflict,
            true => UpdateSecurityResultEnum.Successful
        };
    }
}