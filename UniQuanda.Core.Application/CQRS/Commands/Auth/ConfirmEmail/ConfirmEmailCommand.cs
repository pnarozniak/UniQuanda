using MediatR;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<bool>
{
    public ConfirmEmailCommand(ConfirmEmailRequestDTO request, int idUser, UserAgentInfo userAgentInfo)
    {
        Email = request.Email;
        ConfirmationCode = request.ConfirmationCode;
        IdUser = idUser;
        UserAgentInfo = userAgentInfo;
    }

    public string Email { get; set; }
    public string ConfirmationCode { get; set; }
    public int IdUser { get; set; }
    public UserAgentInfo UserAgentInfo { get; set; }
}