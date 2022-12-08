using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestions
{
    public class GetQuestionsHandler : IRequestHandler<GetQuestionsQuery, GetQuestionsResponseDTO>
    {
        private readonly IQuestionRepository _questionRepository;
        public GetQuestionsHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public async Task<GetQuestionsResponseDTO> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions = await _questionRepository.GetQuestionsAsync(
                request.Take, request.Skip, request.Tags, request.OrderBy, request.SortBy, cancellationToken);
            int? addCount = request.AddCount ? await _questionRepository.GetQuestionsCountAsync(request.Tags, cancellationToken) : null;
            return new()
            {
                Questions = questions.Select(q => new GetQuestionsResponseDTOQuestion()
                {
                    Id = q.Id ?? 0,
                    HasCorrectAnswer = q.HasCorrectAnswer ?? false,
                    Header = q.Header,
                    Html = q.Content.RawText,
                    IsPopular = false, // TODO: implement popular questions
                    Views = q.ViewsCount ?? 0,
                    AnswersCount = q.AnswersCount ?? 0,
                    CreationDate = q.CreatedAt ?? DateTime.Now,
                    TagNames = q.Tags.Select(t => t.Name),
                    User = new GetQuestionsResponseDTOUser()
                    {
                        Id = q.User.Id,
                        Name = q.User.Nickname,
                        ProfilePictureURL = q.User.OptionalInfo.Avatar,
                    }
                }),
                Count = addCount
            };
        }
    }
}
