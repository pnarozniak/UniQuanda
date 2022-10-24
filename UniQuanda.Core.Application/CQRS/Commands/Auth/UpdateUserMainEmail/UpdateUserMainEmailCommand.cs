using MediatR;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateUserMainEmail;

public class UpdateUserMainEmailCommand : IRequest<UpdateResultOfEmailOrPasswordEnum>
{
    public UpdateUserMainEmailCommand(UpdateUserMainEmailRequestDTO request, int idUser)
    {
        NewMainEmail = request.NewMainEmail;
        PlainPassword = request.Password;
        IdUser = idUser;
    }

    public string NewMainEmail { get; set; }
    public string PlainPassword { get; set; }
    public int IdUser { get; set; }
}