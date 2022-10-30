using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmailForUser;

public class AddExtraEmailForUserHandler : IRequestHandler<AddExtraEmailForUserCommand, UpdateSecurityResultEnum>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;
    private readonly IEmailService _emailService;

    public AddExtraEmailForUserHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService,
        IEmailService emailService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
        _emailService = emailService;
    }

    public async Task<UpdateSecurityResultEnum> Handle(AddExtraEmailForUserCommand request, CancellationToken ct)
    {
        var user = await _authRepository.GetUserByIdAsync(request.IdUser, ct);
        if (user is null)
            return UpdateSecurityResultEnum.ContentNotExist;

        if (!_passwordsService.VerifyPassword(request.PlainPassword, user.HashedPassword))
            return UpdateSecurityResultEnum.InvalidPassword;

        var isEmailAvailable = await _authRepository.IsEmailAvailableAsync(null, request.NewExtraEmail, ct);
        if (!isEmailAvailable)
            return UpdateSecurityResultEnum.EmailNotAvailable;

        var addResult = await _authRepository.AddExtraEmailAsync(request.IdUser, request.NewExtraEmail, ct);

        if (addResult == true)
            await _emailService.SendInformationAboutAddNewExtraEmailAsync(user.Emails.SingleOrDefault(e => e.IsMain).Value, request.NewExtraEmail);

        return addResult switch
        {
            null => UpdateSecurityResultEnum.OverLimitOfExtraEmails,
            false => UpdateSecurityResultEnum.DbConflict,
            true => UpdateSecurityResultEnum.Successful
        };
    }
}
