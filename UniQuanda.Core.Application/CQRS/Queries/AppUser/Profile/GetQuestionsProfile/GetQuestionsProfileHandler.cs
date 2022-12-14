using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetQuestionsProfile
{
    public class GetQuestionProfileHandler : IRequestHandler<GetQuestionsProfileQuery, GetQuestionsProfileResponseDTO>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAuthRepository _authRepository;
        public GetQuestionProfileHandler(IQuestionRepository questionRepository, IAuthRepository authRepository)
        {
            _questionRepository = questionRepository;
            _authRepository = authRepository;
        }
        public async Task<GetQuestionsProfileResponseDTO> Handle(GetQuestionsProfileQuery request, CancellationToken ct)
        {
            var user = await _authRepository.GetUserByIdAsync(request.UserId,ct);
            if (user == null) return new()
            {
                Questions = null,
                TotalCount = null
            };
            return new()
            {
                Questions = (await _questionRepository.GetQuestionsOfUserAsync(request.UserId, request.Take, request.Skip, ct))
                    .Select(q => new GetQuestionsProfileResponseDTOQuestion()
                    {
                        Id = q.Id ?? 0,
                        Answers = q.AnswersCount ?? 0,
                        CreatedAt = q.CreatedAt ?? DateTime.Now,
                        Header = q.Header,
                        Html = q.Content.RawText,
                        Views = q.ViewsCount ?? 0,
                        TagNames = q.Tags.Select(t => t.Name)
                    }),
                TotalCount = request.AddCount ? await _questionRepository.GetQuestionsOfUserCountAsync(request.UserId, ct) : null
            };
            
        }
    }
}
