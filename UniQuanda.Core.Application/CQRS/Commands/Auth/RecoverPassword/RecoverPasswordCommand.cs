using MediatR;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.RecoverPassword;

public class RecoverPasswordCommand : IRequest<bool>
{
    public RecoverPasswordCommand(RecoverPasswordDTO request, UserAgentInfo userAgentInfo)
    {
        Email = request.Email;
        UserAgentInfo = userAgentInfo;
    }

    public string Email { get; set; }
    public UserAgentInfo UserAgentInfo { get; set; }
}