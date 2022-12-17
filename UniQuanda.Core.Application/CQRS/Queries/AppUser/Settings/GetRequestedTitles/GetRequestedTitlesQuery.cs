using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetRequestedTitles
{
    public class GetRequestedTitlesQuery : IRequest<IEnumerable<GetRequestedTitlesResponseDTO>>
    {
        public GetRequestedTitlesQuery(GetRequestedTitlesRequestDTO request)
        {
            this.UserId = request.UserId;
        }
        public int UserId { get; set; }
    }
}
