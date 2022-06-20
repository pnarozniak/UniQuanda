using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister
{
    public class ConfirmRegisterCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string ConfirmationCode { get; set; }

        public ConfirmRegisterCommand(ConfirmRegisterRequestDTO request)
        {
            this.Email = request.Email;
            this.ConfirmationCode = request.ConfirmationCode;
        }
    }
}
