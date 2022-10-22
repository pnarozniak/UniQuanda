using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetProfile
{
    public class GetProfileQuery : IRequest<GetProfileResponseDTO?>
    {
        public GetProfileQuery(GetProfileRequestDTO request)
        {
            UserId = request.UserId;
        }
        public int UserId { get; set; }
    }
}