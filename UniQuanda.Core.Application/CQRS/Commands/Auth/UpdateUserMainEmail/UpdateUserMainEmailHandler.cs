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

        if (request.NewMainEmail != null)
        {
            var isEmailAvailable = await _authRepository.IsEmailAvailableAsync(request.IdUser, request.NewMainEmail, ct);
            if (!isEmailAvailable)
                return UpdateResultOfEmailOrPasswordEnum.EmailNotAvailable;

            var idExtreEmail = await _authRepository.GetExtraEmailIdAsync(request.IdUser, request.NewMainEmail, ct);
            if (idExtreEmail is null)
            {
                var updateResultWithValue = await _authRepository.UpdateUserMainEmailAsync(request.IdUser, request.NewMainEmail, ct);
                return updateResultWithValue switch
                {
                    null => UpdateResultOfEmailOrPasswordEnum.ContentNotExist,
                    false => UpdateResultOfEmailOrPasswordEnum.NotSuccessful,
                    true => UpdateResultOfEmailOrPasswordEnum.Successful
                };
            }
            else
            {
                request.IdExtraEmail = idExtreEmail;
            }
        }

        var updateWithExtraEmailResult = await _authRepository.UpdateUserMainEmailByExtraEmail(request.IdUser, request.IdExtraEmail.Value, ct);
        return updateWithExtraEmailResult switch
        {
            null => UpdateResultOfEmailOrPasswordEnum.ContentNotExist,
            false => UpdateResultOfEmailOrPasswordEnum.NotSuccessful,
            true => UpdateResultOfEmailOrPasswordEnum.Successful
        };
    }
}