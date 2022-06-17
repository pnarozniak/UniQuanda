using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailFree
{
    public class IsEmailFreeHandler : IRequestHandler<IsEmailFreeQuery, bool>
    {
        private readonly IAuthRepository _authRepository;
        public IsEmailFreeHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        
        public async Task<bool> Handle(IsEmailFreeQuery request, CancellationToken cancellationToken)
        {
            return !await _authRepository.IsEmailUsedAsync(request.Email);
        }
    }
}
