using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Test.GetTest
{
    public class GetTestHandler : IRequestHandler<GetTestQuery, GetTestResponseDTO?>
    {
				private readonly ITestRepository _testRepository;
        public GetTestHandler(ITestRepository testRepository)
        {
						_testRepository = testRepository;
        }

				public async Task<GetTestResponseDTO?> Handle(GetTestQuery request, CancellationToken ct)
				{
						var test = await _testRepository.GetTestAsync(request.IdTest, ct);
						if (test is null) return null;

						return new GetTestResponseDTO
						{
								CreatedAt = test.CreatedAt,
								IsCreator = test.IdCreator == request.IdUser,
								IsFinished = test.IsFinished,
								Questions = test.Questions!.Select(q => 
										new GetTestResponseDTO.TestQuestionResponseDTO
										{
												Id = q.Id,
												CreatedAt = q.CreatedAt,
												Header = q.Header,
												HTML = q.HTML,
												Answer = new GetTestResponseDTO.TestAnswerResponseDTO
													{
														Id = q.Answer.Id,
														HTML = q.Answer.HTML,
														CreatedAt = q.Answer.CreatedAt,
														CommentsCount = q.Answer.CommentsCount
													}
										}),
									Tags = test.Tags!.Select(t => new GetTestResponseDTO.GetTestResponseDTOTag
                                    {
											Id = t.Id,
											Name = t.Name,
									})
						};
				}
		}
}
