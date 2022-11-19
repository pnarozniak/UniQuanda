using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateMainEmail;

public class UpdateMainEmailHandler : IRequestHandler<UpdateMainEmailCommand, UpdateMainEmailResponseDTO>
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

    public async Task<UpdateMainEmailResponseDTO> Handle(UpdateMainEmailCommand request, CancellationToken ct)
    {
        var user = await _authRepository.GetUserWithEmailsByIdAsync(request.IdUser, ct);
        if (user is null)
            return new UpdateMainEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.ContentNotExist };

        if (!_passwordsService.VerifyPassword(request.PlainPassword, user.HashedPassword))
            return new UpdateMainEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.InvalidPassword };

        bool? updateResult;
        if (request.IdExtraEmail != null)
        {
            updateResult = await UpdateUserMainEmailByExistingOne(request, ct);
        }
        else
        {
            var isEmailAvailable = await _authRepository.IsEmailAvailableAsync(request.IdUser, request.NewMainEmail, ct);
            if (!isEmailAvailable)
                return new UpdateMainEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.EmailNotAvailable };

            updateResult = await UpdateUserMainEmailByNewOne(request, ct);
        }

        return updateResult switch
        {
            null => new UpdateMainEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.ContentNotExist },
            false => new UpdateMainEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.UnSuccessful },
            true => new UpdateMainEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.Successful }
        };
    }

    private async Task<bool?> UpdateUserMainEmailByExistingOne(UpdateMainEmailCommand request, CancellationToken ct)
    {
        var userEmailToConfirm = new UserEmailToConfirm
        {
            IdUser = request.IdUser,
            IdEmail = request.IdExtraEmail.Value,
            ConfirmationToken = _tokensService.GenerateNewEmailConfirmationToken(),
            ExistsUntil = DateTime.UtcNow.AddHours(_expirationService.GetEmailConfirmationExpirationInHours()),
        };
        return await _authRepository.UpdateUserMainEmailByExtraEmailAsync(userEmailToConfirm, ct);
    }

    private async Task<bool?> UpdateUserMainEmailByNewOne(UpdateMainEmailCommand request, CancellationToken ct)
    {
        bool? updateResult;

        var (isEmailConnected, idEmail) = await _authRepository.GetExtraEmailIdAsync(request.IdUser, request.NewMainEmail, ct);
        if (isEmailConnected && idEmail != null)
        {
            request.IdExtraEmail = idEmail;
            updateResult = await UpdateUserMainEmailByExistingOne(request, ct);
        }
        else if (isEmailConnected && idEmail == null)
        {
            updateResult = true;
        }
        else
        {
            updateResult = await AddUserMainEmailToConfirm(request, ct);
        }
        return updateResult;
    }


    private async Task<bool?> AddUserMainEmailToConfirm(UpdateMainEmailCommand request, CancellationToken ct)
    {
        var userEmailToConfirm = new UserEmailToConfirm
        {
            IdUser = request.IdUser,
            Email = request.NewMainEmail,
            ConfirmationToken = _tokensService.GenerateNewEmailConfirmationToken(),
            ExistsUntil = DateTime.UtcNow.AddHours(_expirationService.GetEmailConfirmationExpirationInHours()),
        };
        var addResult = await _authRepository.AddUserMainEmailToConfirmAsync(userEmailToConfirm, ct);

        if (addResult == true)
            await _emailService.SendEmailWithEmailConfirmationLinkAsync(userEmailToConfirm.Email, userEmailToConfirm.ConfirmationToken);
        return addResult;
    }
}