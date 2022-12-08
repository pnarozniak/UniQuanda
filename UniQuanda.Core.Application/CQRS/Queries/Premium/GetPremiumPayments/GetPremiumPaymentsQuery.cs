using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Premium.GetPremiumPayments;

public class GetPremiumPaymentsQuery : IRequest<GetPremiumPaymentsResponseDTO>
{
    public GetPremiumPaymentsQuery(GetPremiumPaymentsRequestDTO request, int idUser)
    {
        GetAll = request.GetAll;
        IdUser = idUser;
    }

    public bool GetAll { get; set; }
    public int IdUser { get; set; }
}