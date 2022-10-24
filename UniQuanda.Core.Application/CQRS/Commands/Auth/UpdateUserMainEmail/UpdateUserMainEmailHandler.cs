using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateUserMainEmail;

public class UpdateUserMainEmailHandler : IRequestHandler<UpdateUserMainEmailCommand, UpdateResultOfEmailOrPasswordEnum>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;

    public UpdateUserMainEmailHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
    }

    public async Task<UpdateResultOfEmailOrPasswordEnum> Handle(UpdateUserMainEmailCommand request, CancellationToken ct)
    {
        var hashedPassword = await _authRepository.GetUserHashedPasswordByIdAsync(request.IdUser, ct);
        if (hashedPassword == null)
            return UpdateResultOfEmailOrPasswordEnum.ContentNotExist;

        if (!_passwordsService.VerifyPassword(request.PlainPassword, hashedPassword))
            return UpdateResultOfEmailOrPasswordEnum.InvalidPassword;

        var isEmailConnectedWithUser = await _authRepository.IsEmailConnectedWithUserAsync(request.IdUser, request.NewMainEmail, ct);
        if (!isEmailConnectedWithUser)
            return UpdateResultOfEmailOrPasswordEnum.EmailNotConnected;

        var updateResult = await _authRepository.UpdateUserMainEmailAsync(request.IdUser, request.NewMainEmail, ct);
        return updateResult switch
        {
            null => UpdateResultOfEmailOrPasswordEnum.ContentNotExist,
            false => UpdateResultOfEmailOrPasswordEnum.NotSuccessful,
            true => UpdateResultOfEmailOrPasswordEnum.Successful
        };
    }
}