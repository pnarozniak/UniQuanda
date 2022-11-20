using MediatR;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResetPasword;

public class ResetPasswordCommand : IRequest<bool>
{
    public ResetPasswordCommand(ResetPaswordDTO request, UserAgentInfo userAgentInfo)
    {
        RecoveryToken = request.RecoveryToken;
        Email = request.Email;
        NewPassword = request.NewPassword;
        UserAgentInfo = userAgentInfo;
    }

    public string RecoveryToken { get; set; }
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public UserAgentInfo UserAgentInfo { get; set; }
}