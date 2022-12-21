using MediatR;
using System.Text.RegularExpressions;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmOAuthRegister;

public class ConfirmOAuthRegisterHandler : IRequestHandler<ConfirmOAuthRegisterCommand, ConfirmOAuthRegisterResponseDTO?>
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokensService _tokensService;
    private readonly IEmailService _emailService;
    private readonly IUniversityRepository _universityRepository;
    private readonly IRoleRepository _roleRepository;
    public ConfirmOAuthRegisterHandler(
        IAuthRepository authRepository,
        ITokensService tokensService,
        IEmailService emailService,
        IUniversityRepository universityRepository,
        IRoleRepository roleRepository
    )
    {
        _authRepository = authRepository;
        _tokensService = tokensService;
        _emailService = emailService;
        _universityRepository = universityRepository;
        _roleRepository = roleRepository;
    }

    public async Task<ConfirmOAuthRegisterResponseDTO?> Handle(ConfirmOAuthRegisterCommand request, CancellationToken ct)
    {
        var idUser = await _authRepository.ConfirmOAuthRegisterAsync(request.ConfirmationCode, request.NewUser, ct);
        if (idUser is null) return null;

        var user = await _authRepository.GetUserWithEmailsByIdAsync(idUser.Value!, ct);
        if (user is null) return null;

        var universities = await _universityRepository.GetUniversitiresWhereUserIsNotPresentAsync(idUser ?? 0, ct);
        foreach (var university in universities)
        {
            var regex = new Regex(university.Regex, RegexOptions.IgnoreCase);
            var addEduRole = false;
            if (regex.IsMatch(user?.Emails.ToList()[0].Value ?? ""))
            {
                await _universityRepository.AddUserToUniversityAsync(idUser ?? 0, university.Id, ct);
                addEduRole = true;
            }
            if (addEduRole)
            {
                await _roleRepository.AssignAppRoleToUserAsync(idUser ?? 0, new AppRole() { Value = AppRole.EduUser }, null, ct);
            }
        }
        var authRoles = new List<AuthRole>() { new AuthRole() { Value = AuthRole.OAuthAccount } };
        var appRoles = await _roleRepository.GetNotExpiredUserRolesAsync(idUser ?? 0, ct);

        await this._emailService.SendOAuthRegisterSuccessEmail(user.Emails.Where(e => e.IsMain).SingleOrDefault()!.Value);
        return new ConfirmOAuthRegisterResponseDTO
        {
            AccessToken = _tokensService.GenerateAccessToken(idUser.Value!, appRoles, authRoles)
        };
    }
}