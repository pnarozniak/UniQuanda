using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Login;

public class LoginCommand : IRequest<LoginResponseDTO>
{
    public LoginCommand(LoginRequestDTO request)
    {
        Email = request.Email;
        Password = request.Password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}