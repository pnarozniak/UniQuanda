using MediatR;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResendEmailWithConfirmationEmailLink;

public class ResendEmailWithConfirmationEmailLinkCommand : IRequest<bool>
{
    public ResendEmailWithConfirmationEmailLinkCommand(int idUser, UserAgentInfo userAgentInfo)
    {
        IdUser = idUser;
        UserAgentInfo = userAgentInfo;
    }

    public int IdUser { get; set; }
    public UserAgentInfo UserAgentInfo { get; set; }
}