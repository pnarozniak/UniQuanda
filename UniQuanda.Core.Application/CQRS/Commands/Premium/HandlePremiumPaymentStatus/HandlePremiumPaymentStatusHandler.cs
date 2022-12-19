using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Premium.HandlePremiumPaymentStatus;

public class HandlePremiumPaymentStatusHandler : IRequestHandler<HandlePremiumPaymentStatusCommand, HandlePremiumPaymentStatusResponseDTO>
{
    private readonly IPaymentService _paymentService;
    private readonly IPremiumPaymentRepository _premiumPaymentRepository;
    private readonly ITokensService _tokensService;
    private readonly IAuthRepository _authRepository;
    private readonly IRoleRepository _roleRepository;

    public HandlePremiumPaymentStatusHandler(
        IPremiumPaymentRepository premiumPaymentRepository, IPaymentService paymentService, 
        ITokensService tokensService, IAuthRepository authRepository, IRoleRepository roleRepository)
    {
        _premiumPaymentRepository = premiumPaymentRepository;
        _paymentService = paymentService;
        _tokensService = tokensService;
        _authRepository = authRepository;
        _roleRepository = roleRepository;
    }

    //tutaj wstrzyknąć nadanie roli
    public async Task<HandlePremiumPaymentStatusResponseDTO?> Handle(HandlePremiumPaymentStatusCommand request, CancellationToken ct)
    {
        var premiumPaymentId = await _premiumPaymentRepository.GetPremiumPaymentIdAsync(request.IdUser, ct);
        if (premiumPaymentId is null)
            return new() { Status = HandlePremiumPaymentStatusResultEnum.ContentNotExist };

        var token = await _paymentService.GetAuthenticationTokenAsync(ct);
        if (token is null)
            return new() { Status = HandlePremiumPaymentStatusResultEnum.PayUError };

        var order = await _paymentService.GetOrderOfPremiumAsync(token, premiumPaymentId, ct);
        if (order is null)
            return new() { Status = HandlePremiumPaymentStatusResultEnum.PayUError };

        var updateStatus = await _premiumPaymentRepository.UpdatePremiumPaymentAsync(order, ct);
        if (updateStatus == UpdatePremiumPaymentResultEnum.ContentNotExist)
            return new() { Status = HandlePremiumPaymentStatusResultEnum.ContentNotExist };
        else if (updateStatus == UpdatePremiumPaymentResultEnum.PaymentHasStatusNew)
            return new() { Status = HandlePremiumPaymentStatusResultEnum.PaymentHasStatusNew };
        else if (updateStatus == UpdatePremiumPaymentResultEnum.UnSuccessful)
            return new() { Status = HandlePremiumPaymentStatusResultEnum.UnSuccessful };
        var user = await _authRepository.GetUserByIdAsync(request.IdUser, ct);
        if (user == null)
            return new() { Status = HandlePremiumPaymentStatusResultEnum.ContentNotExist };
        
        var authRoles = new List<AuthRole>() { };
        authRoles.Add(user.IsOAuthUser ? new AuthRole() { Value = AuthRole.OAuthAccount } : new AuthRole() { Value = AuthRole.UniquandaAccount });
        var appRoles = await _roleRepository.GetNotExpiredUserRolesAsync(request.IdUser, ct);
        var accessToken = _tokensService.GenerateAccessToken(request.IdUser, appRoles, authRoles);
        var (refreshToken, refreshTokenExp) = _tokensService.GenerateRefreshToken();
        await _authRepository.UpdateUserRefreshTokenAsync(request.IdUser, refreshToken, refreshTokenExp, ct);
        return new()
        {
            Status = HandlePremiumPaymentStatusResultEnum.Successful,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}