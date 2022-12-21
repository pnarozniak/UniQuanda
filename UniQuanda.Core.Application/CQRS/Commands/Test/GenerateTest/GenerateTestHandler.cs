using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Test.GenerateTest
{
    public class GenerateTestHandler : IRequestHandler<GenerateTestCommand, GenerateTestResponseDTO?>
    {
				private readonly ITestRepository _testRepository;

        public GenerateTestHandler(ITestRepository testRepository)
        {
						_testRepository = testRepository;
        }

				public async Task<GenerateTestResponseDTO?> Handle(GenerateTestCommand command, CancellationToken ct)
				{
						var idTest = await _testRepository.GenerateTestAsync(command.IdUser, command.TagsIds, ct);
						if (idTest is null) return null;

						return idTest is null ? null : new GenerateTestResponseDTO { IdTest = idTest.Value };
				}
	}
}
