using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateUserMainEmail;

public class UpdateUserMainEmailHandler : IRequestHandler<UpdateUserMainEmailCommand, UpdateSecurityResultEnum>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;
    private readonly IEmailService _emailService;

    public UpdateUserMainEmailHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService,
        IEmailService emailService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
        _emailService = emailService;
    }

    public async Task<UpdateSecurityResultEnum> Handle(UpdateUserMainEmailCommand request, CancellationToken ct)
    {
        var user = await _authRepository.GetUserByIdAsync(request.IdUser, ct);
        if (user is null)
            return UpdateSecurityResultEnum.ContentNotExist;

        if (!_passwordsService.VerifyPassword(request.PlainPassword, user.HashedPassword))
            return UpdateSecurityResultEnum.InvalidPassword;

        bool? updateResult;
        if (request.IdExtraEmail != null)
        {
            updateResult = await _authRepository.UpdateUserMainEmailByExtraEmail(request.IdUser, request.IdExtraEmail.Value, ct);
        }
        else
        {
            var isEmailAvailable = await _authRepository.IsEmailAvailableAsync(request.IdUser, request.NewMainEmail, ct);
            if (!isEmailAvailable)
                return UpdateSecurityResultEnum.EmailNotAvailable;

            var idExtreEmail = await _authRepository.GetExtraEmailIdAsync(request.IdUser, request.NewMainEmail, ct);
            if (idExtreEmail is not null)
            {
                updateResult = await _authRepository.UpdateUserMainEmailByExtraEmail(request.IdUser, idExtreEmail.Value, ct);
            }
            else
            {
                updateResult = await _authRepository.UpdateUserMainEmailAsync(request.IdUser, request.NewMainEmail, ct);
            }
        }
        if (updateResult == true)
        {
            var newExtraEmail = request.IdExtraEmail != null ? user.Emails.SingleOrDefault(e => e.Id == request.IdExtraEmail).Value : request.NewMainEmail;
            await _emailService.SendInformationAboutUpdateMainEmailAsync(user.Emails.SingleOrDefault(e => e.IsMain).Value, newExtraEmail);
        }

        return updateResult switch
        {
            null => UpdateSecurityResultEnum.ContentNotExist,
            false => UpdateSecurityResultEnum.DbConflict,
            true => UpdateSecurityResultEnum.Successful
        };
    }
}