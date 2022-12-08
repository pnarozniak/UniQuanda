using MediatR;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.Questions
{
    public class GetQuestionsQuery : IRequest<GetQuestionsResponseDTO>
    {
        public GetQuestionsQuery(GetQuestionsRequestDTO request)
        {
            this.Take = request.PageSize;
            this.Skip = (request.Page - 1) * request.PageSize;
            this.AddCount = request.AddCount;
            this.Tags = request.Tags;
            this.OrderBy = request.OrderBy;
            this.SortBy = request.SortBy;
        }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool AddCount { get; set; }
        public IEnumerable<int>? Tags { get; set; }
        public OrderDirectionEnum OrderBy { get; set; }
        public QuestionSortingEnum SortBy { get; set; }
    }
}
