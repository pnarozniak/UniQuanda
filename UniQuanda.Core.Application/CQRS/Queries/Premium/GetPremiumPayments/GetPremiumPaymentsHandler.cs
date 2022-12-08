using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Premium.GetPremiumPayments;

public class GetPremiumPaymentsHandler : IRequestHandler<GetPremiumPaymentsQuery, GetPremiumPaymentsResponseDTO?>
{
    private readonly IPremiumPaymentRepository _premiumPaymentRepository;
    private readonly IProductRepository _productRepository;

    public GetPremiumPaymentsHandler(IPremiumPaymentRepository premiumPaymentRepository, IProductRepository productRepository)
    {
        _premiumPaymentRepository = premiumPaymentRepository;
        _productRepository = productRepository;
    }

    public async Task<GetPremiumPaymentsResponseDTO?> Handle(GetPremiumPaymentsQuery request, CancellationToken ct)
    {
        var result = await _premiumPaymentRepository.GetPremiumPaymentsAsync(request.IdUser, request.GetAll, ct);
        if (result is null)
            return null;
        var premiumPrice = await _productRepository.GetPremiumPriceAsync(ct);
        if (premiumPrice is null)
            return null;
        return new GetPremiumPaymentsResponseDTO
        {
            Nickname = result.Nickname,
            HasPremiumUntil = result.HasPremiumUntil,
            Payments = result.Payments,
            NumberOfPayments = result.NumberOfPayments,
            PremiumPrice = premiumPrice.Value
        };
    }
}