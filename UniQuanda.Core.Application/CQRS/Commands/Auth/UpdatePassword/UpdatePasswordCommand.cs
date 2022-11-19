using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdatePassword;

public class UpdatePasswordCommand : IRequest<UpdatePasswordResponseDTO>
{
    public UpdatePasswordCommand(UpdatePasswordRequestDTO request, int idUser)
    {
        NewPassword = request.NewPassword;
        OldPassword = request.OldPassword;
        IdUser = idUser;
    }

    public string NewPassword { get; set; }
    public string OldPassword { get; set; }
    public int IdUser { get; set; }
}