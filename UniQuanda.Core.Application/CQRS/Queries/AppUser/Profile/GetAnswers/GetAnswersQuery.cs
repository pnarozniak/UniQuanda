using MediatR;
namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetAnswers
{
    public class GetAnswersQuery : IRequest<GetAnswersResponseDTO?>
    {
        public GetAnswersQuery(GetAnswersRequestDTO request)
        {
            UserId = request.UserId;
            Page = request.Page;
            PageSize = request.PageSize;
            AddCount = request.AddCount;
        }
        public int UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool AddCount { get; set; }
    }
}