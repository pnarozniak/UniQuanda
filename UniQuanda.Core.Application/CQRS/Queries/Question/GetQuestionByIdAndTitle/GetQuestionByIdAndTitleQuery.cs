using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Question.GetQuestionByIdAndTitle
{
    public class GetQuestionByIdAndTitleQuery : IRequest<GetQuestionByIdAndTitleResponseDTO>
    {
        public GetQuestionByIdAndTitleQuery(GetQuestionByIdAndTitleRequestDTO request)
        {
            this.Id = request.QuestionId;
            this.Title = request.Title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class GetQuestionByIdAndTitleQueryHandler :
            IRequestHandler<GetQuestionByIdAndTitleQuery, GetQuestionByIdAndTitleResponseDTO>
    {
        private IQuestionRepository _questionRepository;
        public GetQuestionByIdAndTitleQueryHandler(IQuestionRepository questionRepository)
        {
            this._questionRepository = questionRepository;
        }
        public async Task<GetQuestionByIdAndTitleResponseDTO> Handle (
            GetQuestionByIdAndTitleQuery request, 
            CancellationToken cancellationToken
        )
        {
            var question = (await _questionRepository
                .GetQuestionByIdAndTitleAsync(request.Id, request.Title));
            return new GetQuestionByIdAndTitleResponseDTO()
            {
                QuestionId = question.Id,
                Content = question.Content
            };
        }
    }
}
