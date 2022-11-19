using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums.Results;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.DeleteExtraEmail;

public class DeleteExtraEmailHandler : IRequestHandler<DeleteExtraEmailCommand, DeleteExtraEmailResponseDTO>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;
    private readonly IEmailService _emailService;

    public DeleteExtraEmailHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService,
        IEmailService emailService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
        _emailService = emailService;
    }

    public async Task<DeleteExtraEmailResponseDTO> Handle(DeleteExtraEmailCommand request, CancellationToken ct)
    {
        var user = await _authRepository.GetUserWithEmailsByIdAsync(request.IdUser, ct);
        if (user is null)
            return new DeleteExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.ContentNotExist };

        if (!_passwordsService.VerifyPassword(request.Password, user.HashedPassword))
            return new DeleteExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.InvalidPassword };

        var deleteResult = await _authRepository.DeleteExtraEmailAsync(request.IdUser, request.IdExtraEmail, ct);
        if (deleteResult == true)
            await _emailService.SendEmailAboutDeletedExtraEmailAsync(user.Emails.SingleOrDefault(e => e.IsMain).Value, user.Emails.SingleOrDefault(e => e.Id == request.IdExtraEmail).Value);

        return deleteResult switch
        {
            null => new DeleteExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.ContentNotExist },
            false => new DeleteExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.UnSuccessful },
            true => new DeleteExtraEmailResponseDTO { ActionResult = AppUserSecurityActionResultEnum.Successful }
        };
    }
}