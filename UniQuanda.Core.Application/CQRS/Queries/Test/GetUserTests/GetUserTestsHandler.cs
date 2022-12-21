using MediatR;
using UniQuanda.Core.Application.CQRS.Commands.Test.GetUserTests;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Test.GetUserTests
{
    public class GetUserTestsHandler : IRequestHandler<GetUserTestsQuery, GetUserTestsResponseDTO>
    {
				private readonly ITestRepository _testRepository;
        public GetUserTestsHandler(ITestRepository testRepository)
        {
						_testRepository = testRepository;
        }

				public async Task<GetUserTestsResponseDTO> Handle(GetUserTestsQuery request, CancellationToken ct)
				{
						var tests = await _testRepository.GetUserTestsAsync(request.IdUser, ct);
						return new GetUserTestsResponseDTO
						{
								Tests = tests.Select(t => new GetUserTestsResponseDTO.TestResponseDTO
								{
										Id = t.Id,
										CreatedAt = t.CreatedAt,
										IsFinished = t.IsFinished,
										Tags = t.Tags!.Select(t => new GetUserTestsResponseDTO.GetUserTestsResponseDTOTag
                                        {
												Id = t.Id,
												Name = t.Name,
										})
								})
						};
				}
	}
}
