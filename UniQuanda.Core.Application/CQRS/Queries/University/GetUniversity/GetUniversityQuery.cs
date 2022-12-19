using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.University.GetUniversity
{
    public class GetUniversityQuery : IRequest<GetUniversityResponseDTO>
    {
        public GetUniversityQuery(GetUniversityRequestDTO request)
        {
            this.Id = request.Id;
        }
        public int Id { get; set; }
    }
}
