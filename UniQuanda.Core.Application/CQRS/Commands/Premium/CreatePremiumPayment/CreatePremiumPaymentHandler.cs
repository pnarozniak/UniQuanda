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

    public CreatePremiumPaymentHandler(IPaymentService paymentService, IProductRepository productRepository, IPremiumPaymentRepository premiumPaymentRepository)
    {
        _paymentService = paymentService;
        _productRepository = productRepository;
        _premiumPaymentRepository = premiumPaymentRepository;
    }

    public async Task<CreatePremiumPaymentResponseDTO> Handle(CreatePremiumPaymentCommand request, CancellationToken ct)
    {
        var (isUserExists, hasPremiumUntil) = await _premiumPaymentRepository.GetUserPremiumInfoAsync(request.IdUser, ct);
        if (!isUserExists)
            return new() { Status = CreatePremiumPaymentResultEnum.ContentNotExist };

        var paymentUrl = await _premiumPaymentRepository.CheckIfAnyPremiumPaymentIsStartedAsync(request.IdUser, ct);
        if (paymentUrl != null)
            return new() { PaymentUrl = paymentUrl, Status = CreatePremiumPaymentResultEnum.Successful };

        if (hasPremiumUntil < DateTime.UtcNow)
            request.IsContinuationPremium = false;
        if ((hasPremiumUntil != null && !request.IsContinuationPremium) || (hasPremiumUntil > DateTime.UtcNow.AddMonths(1) && request.IsContinuationPremium))
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
