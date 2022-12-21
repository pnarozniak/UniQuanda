using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Shared.Models;

namespace UniQuanda.Core.Application.CQRS.Queries.Permission.AskQuestionPermission
{
    public class AskQuestionPermissionHandler : IRequestHandler<AskQuestionPermission, LimitCheckResponseDTO?>
    {
        private readonly IRoleRepository _roleRepository;
        public AskQuestionPermissionHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<LimitCheckResponseDTO?> Handle(AskQuestionPermission request, CancellationToken cancellationToken)
        {
            var limits = await _roleRepository.GetExecutesOfPermissionByUserAsync(request.UserId, "ask-question", cancellationToken);
            return new LimitCheckResponseDTO
            {
                UsedTimes = limits.usedAmount,
                MaxTimes = limits.maxAmount,
                ShortestRefreshPeriod = limits.closestClearInterval
            };
        }
    }
}
