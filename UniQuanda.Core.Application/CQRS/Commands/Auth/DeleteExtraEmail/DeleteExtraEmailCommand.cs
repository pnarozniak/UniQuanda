using MediatR;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.DeleteExtraEmail;

public class DeleteExtraEmailCommand : IRequest<DeleteExtraEmailResponseDTO>
{
    public DeleteExtraEmailCommand(DeleteExtraEmailRequestDTO request, int idUser, UserAgentInfo userAgentInfo)
    {
        IdExtraEmail = request.IdExtraEmail.Value;
        Password = request.Password;
        IdUser = idUser;
        UserAgentInfo = userAgentInfo;
    }

    public int IdExtraEmail { get; set; }
    public string Password { get; set; }
    public int IdUser { get; set; }
    public UserAgentInfo UserAgentInfo { get; set; }
}
