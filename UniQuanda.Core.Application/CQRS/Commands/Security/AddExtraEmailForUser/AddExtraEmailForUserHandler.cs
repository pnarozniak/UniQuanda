using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Security.AddExtraEmailForUser;

public class AddExtraEmailForUserHandler : IRequestHandler<AddExtraEmailForUserCommand, UpdateResultOfEmailOrPasswordEnum>
{
    private readonly ISecurityRepository _securityRepository;
    private readonly IPasswordsService _passwordsService;

    public AddExtraEmailForUserHandler(
        ISecurityRepository securityRepository,
        IPasswordsService passwordsService)
    {
        _securityRepository = securityRepository;
        _passwordsService = passwordsService;
    }

    public async Task<UpdateResultOfEmailOrPasswordEnum> Handle(AddExtraEmailForUserCommand request, CancellationToken ct)
    {
        var hashedPassword = await _securityRepository.GetUserHashedPasswordByIdAsync(request.IdUser, ct);
        if (hashedPassword == null)
            return UpdateResultOfEmailOrPasswordEnum.UserNotExist;

        if (!_passwordsService.VerifyPassword(request.PlainPassword, hashedPassword))
            return UpdateResultOfEmailOrPasswordEnum.InvalidPassword;

        var isEmailAvailable = await _securityRepository.IsEmailAvailableAsync(request.NewExtraEmail, ct);
        if (!isEmailAvailable)
            return UpdateResultOfEmailOrPasswordEnum.EmailNotAvailable;

        var addResult = await _securityRepository.AddExtraEmailAsync(request.IdUser, request.NewExtraEmail, ct);

        return addResult switch
        {
            null => UpdateResultOfEmailOrPasswordEnum.OverLimitOfExtraEmails,
            false => UpdateResultOfEmailOrPasswordEnum.NotSuccessful,
            true => UpdateResultOfEmailOrPasswordEnum.Successful
        };
    }
}
