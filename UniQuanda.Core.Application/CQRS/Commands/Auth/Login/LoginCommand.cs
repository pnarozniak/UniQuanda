using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Login
{
    public class LoginCommand : IRequest<LoginResponseDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginCommand(LoginRequestDTO request)
        {
            this.Email = request.Email;
            this.Password = request.Password;
        }
    }
}
