using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister
{
    public class ConfirmRegisterHandler : IRequestHandler<ConfirmRegisterCommand, bool>
    {
        private readonly IAuthRepository _authRepository;
        public ConfirmRegisterHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        
        public async Task<bool> Handle(ConfirmRegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authRepository.ConfirmUserRegistrationAsync(request.Email, request.ConfirmationCode);
        }
    }
}
