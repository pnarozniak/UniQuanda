using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResendEmailWithConfirmationEmailLink;

public class ResendEmailWithConfirmationEmailLinkCommand : IRequest<bool>
{
    public ResendEmailWithConfirmationEmailLinkCommand(int idUser)
    {
        IdUser = idUser;
    }

    public int IdUser { get; set; }
}