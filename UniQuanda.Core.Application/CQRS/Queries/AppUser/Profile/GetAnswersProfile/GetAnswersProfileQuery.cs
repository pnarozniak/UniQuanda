using MediatR;
namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetAnswersProfile
{
    public class GetAnswersProfileQuery : IRequest<GetAnswersProfileResponseDTO>
    {
        public GetAnswersProfileQuery(GetAnswersProfileRequestDTO request)
        {
            UserId = request.UserId;
            Take = request.PageSize;
            Skip = (request.Page - 1) * request.PageSize;
            AddCount = request.AddCount;
        }
        public int UserId { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool AddCount { get; set; }
    }
}