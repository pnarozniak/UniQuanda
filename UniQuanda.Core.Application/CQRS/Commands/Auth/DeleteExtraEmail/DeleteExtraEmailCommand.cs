using MediatR;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.DeleteExtraEmail;

public class DeleteExtraEmailCommand: IRequest<UpdateResultOfEmailOrPasswordEnum>
{
    public DeleteExtraEmailCommand(DeleteExtraEmailRequestDTO request, int idUser)
    {
        IdExtraEmail = request.IdExtraEmail;
        Password = request.Password;
        IdUser = idUser;
    }

    public int IdExtraEmail { get; set; }
    public string Password { get; set; }
    public int IdUser { get; set; }
}
