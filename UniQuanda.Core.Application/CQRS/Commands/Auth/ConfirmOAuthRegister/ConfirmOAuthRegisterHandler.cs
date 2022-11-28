using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmOAuthRegister;

public class ConfirmOAuthRegisterHandler : IRequestHandler<ConfirmOAuthRegisterCommand, ConfirmOAuthRegisterResponseDTO?>
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokensService _tokensService;
    private readonly IEmailService _emailService;
    public ConfirmOAuthRegisterHandler(
        IAuthRepository authRepository,
        ITokensService tokensService,
        IEmailService emailService
    )
    {
        _authRepository = authRepository;
        _tokensService = tokensService;
        _emailService = emailService;
    }

    public async Task<ConfirmOAuthRegisterResponseDTO?> Handle(ConfirmOAuthRegisterCommand request, CancellationToken ct)
    {
        var idUser = await _authRepository.ConfirmOAuthRegisterAsync(request.ConfirmationCode, request.NewUser, ct);
        if (idUser is null) return null;

        var user = await _authRepository.GetUserWithEmailsByIdAsync(idUser.Value!, ct);
        if (user is null) return null;

        await this._emailService.SendOAuthRegisterSuccessEmail(user.Emails.Where(e => e.IsMain).SingleOrDefault()!.Value);
        return new ConfirmOAuthRegisterResponseDTO
        {
            AccessToken = _tokensService.GenerateAccessToken(idUser.Value!, true)
        };
    }
}