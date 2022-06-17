using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsNicknameFree
{
    public class IsNicknameFreeHandler : IRequestHandler<IsNicknameFreeQuery, bool>
    {
        private readonly IAuthRepository _authRepository;
        public IsNicknameFreeHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        
        public async Task<bool> Handle(IsNicknameFreeQuery request, CancellationToken cancellationToken)
        {
            return !await _authRepository.IsNicknameUsedAsync(request.Nickname);
        }
    }
}
