using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.University.GetUniversityQuestions
{
    public class GetUniversityQuestionsHandler : IRequestHandler<GetUniversityQuestionsQuery, GetUniversityQuestionsResponseDTO>
    {
        private readonly IQuestionRepository _questionRepository;
        public GetUniversityQuestionsHandler(IQuestionRepository questionRepository)
        {
            this._questionRepository = questionRepository;
        }
        public async Task<GetUniversityQuestionsResponseDTO> Handle(GetUniversityQuestionsQuery request, CancellationToken ct)
        {
            return new GetUniversityQuestionsResponseDTO()
            {
                Questions = (await _questionRepository
                    .GetQuestionsOfUniversityAsync(request.UniversityId, request.Take, request.Skip, ct))
                    .Select(q => new GetUniversityQuestionsResponseDTOQuestion()
                    {
                        Id = q.Id ?? 0,
                        Header = q.Header,
                        Html = q.Content.RawText,
                        Views = q.ViewsCount ?? 0,
                        AnswersCount = q.AnswersCount ?? 0,
                        CreationDate = q.CreatedAt ?? DateTime.Now,
                        IsPopular = q.ViewsCount > 1000,
                        HasCorrectAnswer = q.HasCorrectAnswer ?? false,
                        TagNames = q.Tags.Select(t => t.Name),
                        User = new GetUniversityQuestionsResponseDTOUser()
                        {
                            Id = q.User.Id,
                            Name = q.User.Nickname,
                            ProfilePictureURL = q.User.Avatar
                        }
                    }),
                TotalCount = request.AddCount ? await _questionRepository.GetQuestionsOfUniversityCountAsync(request.UniversityId, ct) : null
            };
        }
    }
}
