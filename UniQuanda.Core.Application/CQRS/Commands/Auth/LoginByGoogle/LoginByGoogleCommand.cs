using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.LoginByGoogle;

public class LoginByGoogleCommand : IRequest<string>
{
    public LoginByGoogleCommand(LoginByGoogleRequestDTO request)
    {
        Code = request.Code;
    }

    public string Code { get; set; }
}