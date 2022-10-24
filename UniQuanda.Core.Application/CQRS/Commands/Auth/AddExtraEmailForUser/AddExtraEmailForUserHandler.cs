using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmailForUser;

public class AddExtraEmailForUserHandler : IRequestHandler<AddExtraEmailForUserCommand, UpdateResultOfEmailOrPasswordEnum>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;

    public AddExtraEmailForUserHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
    }

    public async Task<UpdateResultOfEmailOrPasswordEnum> Handle(AddExtraEmailForUserCommand request, CancellationToken ct)
    {
        var hashedPassword = await _authRepository.GetUserHashedPasswordByIdAsync(request.IdUser, ct);
        if (hashedPassword == null)
            return UpdateResultOfEmailOrPasswordEnum.UserNotExist;

        if (!_passwordsService.VerifyPassword(request.PlainPassword, hashedPassword))
            return UpdateResultOfEmailOrPasswordEnum.InvalidPassword;

        var isEmailAvailable = await _authRepository.IsEmailAvailableAsync(request.NewExtraEmail, ct);
        if (!isEmailAvailable)
            return UpdateResultOfEmailOrPasswordEnum.EmailNotAvailable;

        var addResult = await _authRepository.AddExtraEmailAsync(request.IdUser, request.NewExtraEmail, ct);

        return addResult switch
        {
            null => UpdateResultOfEmailOrPasswordEnum.OverLimitOfExtraEmails,
            false => UpdateResultOfEmailOrPasswordEnum.NotSuccessful,
            true => UpdateResultOfEmailOrPasswordEnum.Successful
        };
    }
}
