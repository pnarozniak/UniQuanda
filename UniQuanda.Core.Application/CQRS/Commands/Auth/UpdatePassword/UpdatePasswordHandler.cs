using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums.Results;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordResponseDTO>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;
    private readonly IEmailService _emailService;

    public UpdatePasswordHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService,
        IEmailService emailService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
        _emailService = emailService;
    }

    public async Task<UpdatePasswordResponseDTO> Handle(UpdatePasswordCommand request, CancellationToken ct)
    {
        if (request.NewPassword == request.OldPassword)
            return new UpdatePasswordResponseDTO { ActionResult = AppUserSecurityActionResultEnum.Successful };

        var user = await _authRepository.GetUserWithEmailsByIdAsync(request.IdUser, ct);
        if (user is null)
            return new UpdatePasswordResponseDTO { ActionResult = AppUserSecurityActionResultEnum.ContentNotExist };

        if (!_passwordsService.VerifyPassword(request.OldPassword, user.HashedPassword))
            return new UpdatePasswordResponseDTO { ActionResult = AppUserSecurityActionResultEnum.InvalidPassword };

        request.NewPassword = _passwordsService.HashPassword(request.NewPassword);

        var updateResult = await _authRepository.UpdateUserPasswordAsync(request.IdUser, request.NewPassword, ct);
        if (updateResult == true)
            await _emailService.SendEmailAboutUpdatedPasswordAsync(user.Emails.SingleOrDefault(e => e.IsMain).Value, user.Nickname, request.UserAgentInfo);

        return updateResult switch
        {
            null => new UpdatePasswordResponseDTO { ActionResult = AppUserSecurityActionResultEnum.ContentNotExist },
            false => new UpdatePasswordResponseDTO { ActionResult = AppUserSecurityActionResultEnum.UnSuccessful },
            true => new UpdatePasswordResponseDTO { ActionResult = AppUserSecurityActionResultEnum.Successful }
        };
    }
}