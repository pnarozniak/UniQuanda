using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmEmail;

public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly IAuthRepository _authRepository;
    private readonly IEmailService _emailService;

    public ConfirmEmailHandler(IAuthRepository authRepository, IEmailService emailService)
    {
        _authRepository = authRepository;
        _emailService = emailService;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken ct)
    {
        var user = await _authRepository.GetUserWithEmailsByIdAsync(request.IdUser, ct);
        if (user is null || user.Emails.All(e => e.Value != request.Email))
            return false;

        var oldMainEmail = user.Emails.SingleOrDefault(e => e.IsMain)!.Value;

        var (isSuccess, isMainEmail, idUser) = await _authRepository.ConfirmUserEmailAsync(request.Email, request.ConfirmationCode, ct);
        if (!isSuccess)
            return false;

        if (isMainEmail) 
        {
            var newMainEmail = user.Emails.SingleOrDefault(e => e.IsMain)!.Value;
            await _emailService.SendEmailAboutUpdatedMainEmailAsync(oldMainEmail, newMainEmail, request.UserAgentInfo);
        }
        else 
        {
            await _emailService.SendEmailAboutAddedNewExtraEmailAsync(oldMainEmail, request.Email, request.UserAgentInfo);
        }

        return true;
    }
}