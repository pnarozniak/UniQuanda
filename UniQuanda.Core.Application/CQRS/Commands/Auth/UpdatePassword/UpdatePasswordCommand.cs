using MediatR;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdatePassword;

public class UpdatePasswordCommand : IRequest<UpdatePasswordResponseDTO>
{
    public UpdatePasswordCommand(UpdatePasswordRequestDTO request, int idUser, UserAgentInfo userAgentInfo)
    {
        NewPassword = request.NewPassword;
        OldPassword = request.OldPassword;
        IdUser = idUser;
        UserAgentInfo = userAgentInfo;
    }

    public string NewPassword { get; set; }
    public string OldPassword { get; set; }
    public int IdUser { get; set; }
    public UserAgentInfo UserAgentInfo { get; set; }
}