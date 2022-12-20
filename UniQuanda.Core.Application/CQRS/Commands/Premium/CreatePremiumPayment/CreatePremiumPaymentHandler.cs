using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums.Results;

namespace UniQuanda.Core.Application.CQRS.Commands.Premium.CreatePremiumPayment;

public class CreatePremiumPaymentHandler : IRequestHandler<CreatePremiumPaymentCommand, CreatePremiumPaymentResponseDTO>
{
    private readonly IPaymentService _paymentService;
    private readonly IProductRepository _productRepository;
    private readonly IPremiumPaymentRepository _premiumPaymentRepository;
    private readonly IAuthRepository _authRepository;

    public CreatePremiumPaymentHandler(
        IPaymentService paymentService, IProductRepository productRepository,
        IPremiumPaymentRepository premiumPaymentRepository,
        IAuthRepository authRepository)
    {
        _paymentService = paymentService;
        _productRepository = productRepository;
        _premiumPaymentRepository = premiumPaymentRepository;
        _authRepository = authRepository;
    }

    public async Task<CreatePremiumPaymentResponseDTO> Handle(CreatePremiumPaymentCommand request, CancellationToken ct)
    {
        var dbUser = await _authRepository.GetUserByIdAsync(request.IdUser, ct);
        if (dbUser == null)
            return new() { Status = CreatePremiumPaymentResultEnum.ContentNotExist };

        var paymentUrl = await _premiumPaymentRepository.CheckIfAnyPremiumPaymentIsStartedAsync(request.IdUser, ct);
        if (paymentUrl != null)
            return new() { PaymentUrl = paymentUrl, Status = CreatePremiumPaymentResultEnum.Successful };

        if (dbUser.HasPremiumUntil < DateTime.UtcNow)
            request.IsContinuationPremium = false;
        if ((dbUser.HasPremiumUntil != null && !request.IsContinuationPremium) || (dbUser.HasPremiumUntil > DateTime.UtcNow.AddMonths(1) && request.IsContinuationPremium))
            return new() { Status = CreatePremiumPaymentResultEnum.NotAllowed };

        var token = await _paymentService.GetAuthenticationTokenAsync(ct);
        if (token is null)
            return new() { Status = CreatePremiumPaymentResultEnum.PayUError };

        var premiumPrice = await _productRepository.GetPremiumPriceAsync(ct);
        if (premiumPrice is null)
            return new() { Status = CreatePremiumPaymentResultEnum.ContentNotExist };

        var order = await _paymentService.CreateOrderOfPremiumAsync(premiumPrice!.Value, token, ct);
        if (order is null)
            return new() { Status = CreatePremiumPaymentResultEnum.PayUError };

        var isAdded = await _premiumPaymentRepository.AddPremiumPaymentAsync(order, premiumPrice!.Value, request.IdUser, ct);
        if (!isAdded)
            return new() { Status = CreatePremiumPaymentResultEnum.UnSuccessful };

        return new()
        {
            PaymentUrl = order.RedirectUri,
            Status = CreatePremiumPaymentResultEnum.Successful
        };
    }
}
