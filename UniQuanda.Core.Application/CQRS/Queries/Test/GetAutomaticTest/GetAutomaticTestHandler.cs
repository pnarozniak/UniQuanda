using System.Net;
using MediatR;
using UniQuanda.Core.Application.CQRS.Commands.Test.GetAutomaticTest;
using UniQuanda.Core.Application.CQRS.Queries.Search.Search;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Test.GetAutomaticTest
{
    public class GetAutomaticTestHandler : IRequestHandler<GetAutomaticTestQuery, GetAutomaticTestResponseDTO>
    {
				private readonly ITestRepository _testRepository;
        public GetAutomaticTestHandler(ITestRepository testRepository)
        {
						_testRepository = testRepository;
        }

				public async Task<GetAutomaticTestResponseDTO> Handle(GetAutomaticTestQuery request, CancellationToken ct)
				{
						var questions = await _testRepository.GetAutomaticTestQuestionsAsync(request.TagsIds, ct);
						return new GetAutomaticTestResponseDTO
						{
								Questions = questions.Select(q => 
										new GetAutomaticTestResponseDTO.AutomaticTestQuestionResponseDTO
										{
												Id = q.Id,
												CreatedAt = q.CreatedAt,
												Header = q.Header,
												HTML = q.HTML,
												Answer = new GetAutomaticTestResponseDTO.AutomaticTestAnswerResponseDTO
													{
														Id = q.Answer.Id,
														HTML = q.Answer.HTML,
														CreatedAt = q.Answer.CreatedAt,
														CommentsCount = q.Answer.CommentsCount
													}
										})
						};
				}
		}
}
