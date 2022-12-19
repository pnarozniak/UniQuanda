using MediatR;
using UniQuanda.Core.Application.CQRS.Queries.Profile.GetAnswersProfile;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetAnswers
{
    public class GetAnswersProfileHandler : IRequestHandler<GetAnswersProfileQuery, GetAnswersProfileResponseDTO>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IAuthRepository _authRepository;
        public GetAnswersProfileHandler(IAnswerRepository answerRepository, IAuthRepository authRepository)
        {
            _answerRepository = answerRepository;
            _authRepository = authRepository;
        }
        public async Task<GetAnswersProfileResponseDTO> Handle(GetAnswersProfileQuery request, CancellationToken ct)
        {
            var user = await _authRepository.GetUserByIdAsync(request.UserId, ct);
            if (user == null) return new()
            {
                Answers = null,
                TotalCount = null
            };
            return new()
            {
                Answers = (await _answerRepository.GetAnswersOfUserAsync(request.UserId, request.Take, request.Skip, ct))
                    .Select(a => new GetAnswersProfileResponseDTOAnswer()
                    {
                        Id = a.Id ?? 0,
                        ParentId = a.ParentAnswerId,
                        Header = a.Question.Header,
                        QuestionId = a.Question.Id ?? 0,
                        Html = a.Content.RawText,
                        Likes = a.Likes ?? 0,
                        IsCorrect = a.IsCorrect ?? false,
                        CreatedAt = a.CreatedAt ?? DateTime.Now,
                        TagNames = a.Question.Tags.Select(t => t.Name)
                    }),
                TotalCount = request.AddCount ? await _answerRepository.GetAnswersOfUserCountAsync(request.UserId, ct) : null
            };

        }
    }
}
