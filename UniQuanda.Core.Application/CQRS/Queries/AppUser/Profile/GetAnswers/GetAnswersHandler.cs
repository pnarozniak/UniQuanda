using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetAnswers
{
    public class GetAnswersProfileHandler : IRequestHandler<GetAnswersQuery, GetAnswersResponseDTO>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IAuthRepository _authRepository;
        public GetAnswersProfileHandler(IAnswerRepository answerRepository, IAuthRepository authRepository)
        {
            _answerRepository = answerRepository;
            _authRepository = authRepository;
        }
        public async Task<GetAnswersResponseDTO> Handle(GetAnswersQuery request, CancellationToken ct)
        {
            var user = await _authRepository.GetUserByIdAsync(request.UserId,ct);
            if (user == null) return new()
            {
                Answers = null,
                TotalCount = null
            };
            return new()
            {
                Answers = /*(await _answerRepository.GetAnswersOfUserAsync(request.UserId, request.Page, request.PageSize, ct))
                    .Select(q => new GetAnswersResponseDTOAnswer()
                    {
                        Id = q.Id ?? 0,
                        Answers = q.AnswersCount ?? 0,
                        CreatedAt = q.CreatedAt ?? DateTime.Now,
                        Header = q.Header,
                        Html = q.Content.RawText,
                        Views = q.ViewsCount ?? 0,
                        Tags = q.Tags.Select(t => new GetQuestionsResponseDTOTag() { 
                            Id = t.Id,
                            Name = t.Name
                        })
                    }),*/null, //Po dostarczeniu modelu przekazywanego do szczegółów pytania będzie zaimplementowane
                TotalCount = request.AddCount ? await _answerRepository.GetAnswersOfUserCountAsync(request.UserId, ct) : null
            };
            
        }
    }
}
