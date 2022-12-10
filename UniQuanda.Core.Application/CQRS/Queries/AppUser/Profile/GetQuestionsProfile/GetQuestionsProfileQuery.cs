using MediatR;
namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetQuestionsProfile
{
    public class GetQuestionsProfileQuery : IRequest<GetQuestionsProfileResponseDTO?>
    {
        public GetQuestionsProfileQuery(GetQuestionsProfileRequestDTO request)
        {
            UserId = request.UserId;
            AddCount = request.AddCount;
            Take = request.PageSize;
            Skip = (request.Page -1) * request.PageSize;
        }
        public int UserId { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool AddCount { get; set; }
    }
}