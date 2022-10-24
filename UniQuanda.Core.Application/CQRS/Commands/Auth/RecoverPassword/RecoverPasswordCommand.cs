using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.RecoverPassword;

public class RecoverPasswordCommand : IRequest<bool>
{
    public RecoverPasswordCommand(RecoverPasswordDTO request)
    {
        Email = request.Email;
    }

    public string Email { get; set; }
}