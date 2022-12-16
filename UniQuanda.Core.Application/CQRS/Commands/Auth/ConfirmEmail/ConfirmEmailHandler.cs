using MediatR;
using System.Text.RegularExpressions;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmEmail;

public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly IAuthRepository _authRepository;
    private readonly IEmailService _emailService;
    private readonly IUniversityRepository _universityRepository;

    public ConfirmEmailHandler(IAuthRepository authRepository, IEmailService emailService, IUniversityRepository universityRepository)
    {
        _authRepository = authRepository;
        _emailService = emailService;
        _universityRepository = universityRepository;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken ct)
    {
        var oldMainEmail = await _authRepository.GetMainEmailByEmailToConfirmAsync(request.Email, ct);
        if (oldMainEmail is null)
            return false;

        var (isSuccess, isMainEmail) = await _authRepository.ConfirmUserEmailAsync(request.Email, request.ConfirmationCode, ct);
        if (!isSuccess)
            return false;

        var idUser = await _authRepository.GetUserIdByEmailAsync(request.Email, ct);

        var universities = await _universityRepository.GetUniversitiresWhereUserIsNotPresentAsync(idUser, ct);
        foreach (var university in universities)
        {
            var regex = new Regex(@university.Regex, RegexOptions.IgnoreCase);
            if (regex.IsMatch(request.Email))
            {
                await _universityRepository.AddUserToUniversityAsync(idUser, university.Id, ct);
            }
        }
        
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