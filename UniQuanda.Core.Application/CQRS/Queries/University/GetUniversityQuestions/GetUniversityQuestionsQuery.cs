using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.University.GetUniversityQuestions
{
    public class GetUniversityQuestionsQuery : IRequest<GetUniversityQuestionsResponseDTO>
    {
        public GetUniversityQuestionsQuery(GetUniversityQuestionsRequestDTO request)
        {
            UniversityId = request.UniversityId;
            Take = request.PageSize;
            Skip = (request.Page - 1) * request.PageSize;
            AddCount = request.AddCount;
        }

        public int UniversityId { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool AddCount { get; set; }
    }
}
