using MediatR;
using UniQuanda.Core.Application.CQRS.Commands.Test.FinishTest;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Test.GenerateTest
{
    public class FinishTestHandler : IRequestHandler<FinishTestCommand, bool>
    {
				private readonly ITestRepository _testRepository;

        public FinishTestHandler(ITestRepository testRepository)
        {
						_testRepository = testRepository;
        }

				public async Task<bool> Handle(FinishTestCommand command, CancellationToken ct)
				{
						var success = await _testRepository.MarkTestAsFinishedAsync(command.IdUser, command.IdTest, ct);
						return success;
				}
	}
}
