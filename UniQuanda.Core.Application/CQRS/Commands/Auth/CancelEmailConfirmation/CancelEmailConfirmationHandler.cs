using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.CancelEmailConfirmation;

public class CancelEmailConfirmationHandler : IRequestHandler<CancelEmailConfirmationCommand, bool>
{
    private readonly IAuthRepository _authRepository;

    public CancelEmailConfirmationHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<bool> Handle(CancelEmailConfirmationCommand request, CancellationToken ct)
    {
        return await _authRepository.CancelEmailConfirmationActionAsync(request.IdUser, ct);
    }
}