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

        var (isSuccess, isMainEmail, idUser) = await _authRepository.ConfirmUserEmailAsync(request.Email, request.ConfirmationCode, ct);
        if (isSuccess)
        {
            var user = await _authRepository.GetUserWithEmailsByIdAsync(idUser.Value, ct);
            if (user is null)
                return false;
            var mainEmail = user.Emails.SingleOrDefault(e => e.IsMain).Value;
            if (isMainEmail)
                await _emailService.SendInformationAboutUpdateMainEmailAsync(mainEmail, mainEmail);
            else
                await _emailService.SendInformationAboutAddNewExtraEmailAsync(mainEmail, request.Email);
        }

        return isSuccess;
    }
}