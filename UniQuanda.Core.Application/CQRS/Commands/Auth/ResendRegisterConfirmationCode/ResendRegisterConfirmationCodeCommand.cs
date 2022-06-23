using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResendRegisterConfirmationCode;

public class ResendRegisterConfirmationCodeCommand : IRequest<bool>
{
    public ResendRegisterConfirmationCodeCommand(ResendRegisterConfirmationCodeRequestDTO request)
    {
        Email = request.Email;
    }

    public string Email { get; set; }
}