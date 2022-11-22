using MediatR;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateMainEmail;

public class UpdateMainEmailCommand : IRequest<UpdateMainEmailResponseDTO>
{
    public UpdateMainEmailCommand(UpdateMainEmailRequestDTO request, int idUser, UserAgentInfo userAgentInfo)
    {
        NewMainEmail = IdExtraEmail == null ? request.NewMainEmail : null;
        IdExtraEmail = request.IdExtraEmail;
        PlainPassword = request.Password;
        IdUser = idUser;
        UserAgentInfo = userAgentInfo;
    }

    public string? NewMainEmail { get; set; }
    public int? IdExtraEmail { get; set; }
    public string PlainPassword { get; set; }
    public int IdUser { get; set; }
    public UserAgentInfo UserAgentInfo { get; set; }
}