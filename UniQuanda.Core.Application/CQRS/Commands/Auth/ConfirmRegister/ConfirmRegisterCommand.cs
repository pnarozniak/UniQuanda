using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister;

public class ConfirmRegisterCommand : IRequest<bool>
{
    public ConfirmRegisterCommand(ConfirmRegisterRequestDTO request)
    {
        Email = request.Email;
        ConfirmationCode = request.ConfirmationCode;
    }

    public string Email { get; set; }
    public string ConfirmationCode { get; set; }
}