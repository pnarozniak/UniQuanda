using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Security.UpdateUserMainEmail;

public class UpdateUserMainEmailHandler : IRequestHandler<UpdateUserMainEmailCommand, UpdateResultOfEmailOrPasswordEnum>
{
    private readonly ISecurityRepository _securityRepository;
    private readonly IPasswordsService _passwordsService;

    public UpdateUserMainEmailHandler(
        ISecurityRepository securityRepository,
        IPasswordsService passwordsService)
    {
        _securityRepository = securityRepository;
        _passwordsService = passwordsService;
    }

    public async Task<UpdateResultOfEmailOrPasswordEnum> Handle(UpdateUserMainEmailCommand request, CancellationToken ct)
    {
        var hashedPassword = await _securityRepository.GetUserHashedPasswordByIdAsync(request.IdUser, ct);
        if (hashedPassword == null)
            return UpdateResultOfEmailOrPasswordEnum.UserNotExist;

        var isEmailConnectedWithUser = await _securityRepository.IsEmailConnectedWithUserAsync(request.IdUser, request.NewMainEmail, ct);
        if (!isEmailConnectedWithUser)
            return UpdateResultOfEmailOrPasswordEnum.EmailIsNotConnected;

        if (!_passwordsService.VerifyPassword(request.PlainPassword, hashedPassword))
            return UpdateResultOfEmailOrPasswordEnum.InvalidPassword;

        var updateResult = await _securityRepository.UpdateUserMainEmailAsync(request.IdUser, request.NewMainEmail, ct);
        return updateResult switch
        {
            null => UpdateResultOfEmailOrPasswordEnum.UserNotExist,
            false => UpdateResultOfEmailOrPasswordEnum.NotSuccessful,
            true => UpdateResultOfEmailOrPasswordEnum.Successful
        };
    }
}