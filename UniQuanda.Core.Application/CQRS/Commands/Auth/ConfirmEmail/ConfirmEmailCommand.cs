using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<bool>
{
    public ConfirmEmailCommand(ConfirmEmailRequestDTO request)
    {
        Email = request.Email;
        ConfirmationCode = request.ConfirmationCode;
    }

    public string Email { get; set; }
    public string ConfirmationCode { get; set; }
}