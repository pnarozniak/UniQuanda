using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResendRegisterConfirmationCode
{
    public class ResendRegisterConfirmationCodeCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public ResendRegisterConfirmationCodeCommand(ResendRegisterConfirmationCodeRequestDTO request)
        {
            this.Email = request.Email;
        }
    }
}
