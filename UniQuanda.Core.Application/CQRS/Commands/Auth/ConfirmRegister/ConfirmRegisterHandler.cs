using MediatR;
using System.Text.RegularExpressions;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister;

public class ConfirmRegisterHandler : IRequestHandler<ConfirmRegisterCommand, bool>
{
    private readonly IAuthRepository _authRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IRoleRepository _roleRepository;

    public ConfirmRegisterHandler(IAuthRepository authRepository, IUniversityRepository universityRepository, IRoleRepository roleRepository)
    {
        _authRepository = authRepository;
        _universityRepository = universityRepository;
        _roleRepository = roleRepository;
    }

    public async Task<bool> Handle(ConfirmRegisterCommand request, CancellationToken ct)
    {
        var result = await _authRepository.ConfirmUserRegistrationAsync(request.Email, request.ConfirmationCode, ct);
        if (!result)
        {
            return false;
        }
        var uid = await _authRepository.GetUserIdByEmailAsync(request.Email, ct);
        var universities = await _universityRepository.GetUniversitiresWhereUserIsNotPresentAsync(uid, ct);
        var addEduRole = false;
        foreach (var university in universities)
        {
            var regex = new Regex(university.Regex, RegexOptions.IgnoreCase);
            if (regex.IsMatch(request.Email))
            {
                result = await _universityRepository.AddUserToUniversityAsync(uid, university.Id, ct);
                addEduRole = true;
                if (!result)
                {
                    return false;
                }
            }
        }
        if (addEduRole)
            await _roleRepository.AssignAppRoleToUserAsync(uid, new AppRole() { Value = AppRole.EduUser }, null, ct);
        return true;

    }
}