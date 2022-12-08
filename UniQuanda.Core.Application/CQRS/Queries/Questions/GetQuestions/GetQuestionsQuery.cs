using MediatR;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestions
{
    public class GetQuestionsQuery : IRequest<GetQuestionsResponseDTO>
    {
        public GetQuestionsQuery(GetQuestionsRequestDTO request)
        {
            Take = request.PageSize;
            Skip = (request.Page - 1) * request.PageSize;
            AddCount = request.AddCount;
            Tags = request.Tags;
            OrderBy = request.OrderBy;
            SortBy = request.SortBy;
        }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool AddCount { get; set; }
        public IEnumerable<int>? Tags { get; set; }
        public OrderDirectionEnum OrderBy { get; set; }
        public QuestionSortingEnum SortBy { get; set; }
    }
}
