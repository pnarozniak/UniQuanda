using MediatR;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<bool>
{
    public ConfirmEmailCommand(ConfirmEmailRequestDTO request, UserAgentInfo userAgentInfo)
    {
        Email = request.Email;
        ConfirmationCode = request.ConfirmationCode;
        UserAgentInfo = userAgentInfo;
    }

    public string Email { get; set; }
    public string ConfirmationCode { get; set; }
    public UserAgentInfo UserAgentInfo { get; set; }
}