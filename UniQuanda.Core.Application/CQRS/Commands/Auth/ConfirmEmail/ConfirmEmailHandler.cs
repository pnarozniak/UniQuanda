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
        var oldMainEmail = await _authRepository.GetMainEmailByEmailToConfirmAsync(request.Email, ct);
        if (oldMainEmail is null)
            return false;

        var (isSuccess, isMainEmail) = await _authRepository.ConfirmUserEmailAsync(request.Email, request.ConfirmationCode, ct);
        if (!isSuccess)
            return false;

        if (isMainEmail)
        {
            await _emailService.SendEmailAboutUpdatedMainEmailAsync(oldMainEmail, request.Email, request.UserAgentInfo);
        }
        else
        {
            await _emailService.SendEmailAboutAddedNewExtraEmailAsync(oldMainEmail, request.Email, request.UserAgentInfo);
        }

        return true;
    }
}