using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Question.GetQuestions
{
    public class GetQuestionsQuery : IRequest<IEnumerable<GetQuestionsResponseDTO>>
    {
        
    }

    public class GetQuestionsHandler : IRequestHandler<GetQuestionsQuery, IEnumerable<GetQuestionsResponseDTO>>
    {
        private readonly IQuestionRepository _repository;
        public GetQuestionsHandler(IQuestionRepository repository)
        {
            this._repository = repository;
        }
        public async Task<IEnumerable<GetQuestionsResponseDTO>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            return (await this._repository.GetQuestionsAsync()).Select(q => new GetQuestionsResponseDTO()
            {
                QuestionId = q.Id,
                Title = q.Title,
            });
        }
    }
}
