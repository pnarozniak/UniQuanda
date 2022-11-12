using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResendConfirmationEmail;

public class ResendConfirmationEmailCommand : IRequest<bool>
{
    public ResendConfirmationEmailCommand(int idUser)
    {
        IdUser = idUser;
    }

    public int IdUser { get; set; }
}