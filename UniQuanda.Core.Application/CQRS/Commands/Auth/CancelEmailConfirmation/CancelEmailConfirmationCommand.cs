using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.CancelEmailConfirmation;

public class CancelEmailConfirmationCommand : IRequest<bool>
{
    public CancelEmailConfirmationCommand(int idUser)
    {
        IdUser = idUser;
    }

    public int IdUser { get; set; }
}