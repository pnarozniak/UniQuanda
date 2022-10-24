using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.DeleteExtraEmail;

public class DeleteExtraEmailHandler : IRequestHandler<DeleteExtraEmailCommand, UpdateResultOfEmailOrPasswordEnum>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;

    public DeleteExtraEmailHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
    }

    public async Task<UpdateResultOfEmailOrPasswordEnum> Handle(DeleteExtraEmailCommand request, CancellationToken ct)
    {
        var hashedPassword = await _authRepository.GetUserHashedPasswordByIdAsync(request.IdUser, ct);
        if (hashedPassword == null)
            return UpdateResultOfEmailOrPasswordEnum.ContentNotExist;

        if (!_passwordsService.VerifyPassword(request.Password, hashedPassword))
            return UpdateResultOfEmailOrPasswordEnum.InvalidPassword;

        var deleteResult = await _authRepository.DeleteExtraEmailAsync(request.IdUser, request.IdExtraEmail, ct);
        return deleteResult switch
        {
            null => UpdateResultOfEmailOrPasswordEnum.ContentNotExist,
            false => UpdateResultOfEmailOrPasswordEnum.NotSuccessful,
            true => UpdateResultOfEmailOrPasswordEnum.Successful
        };
    }
}