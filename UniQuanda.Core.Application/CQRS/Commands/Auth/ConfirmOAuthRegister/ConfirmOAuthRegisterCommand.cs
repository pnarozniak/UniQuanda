using MediatR;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmOAuthRegister;

public class ConfirmOAuthRegisterCommand : IRequest<ConfirmOAuthRegisterResponseDTO?>
{
    public ConfirmOAuthRegisterCommand(ConfirmOAuthRegisterRequestDTO request)
    {
        ConfirmationCode = request.ConfirmationCode;
        NewUser = new NewUserEntity
        {
            Nickname = request.Nickname,
            OptionalInfo = new UserOptionalInfo
            {
                Birthdate = request.Birthdate,
                City = request.City,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            }
        };
    }

    public string ConfirmationCode { get; set; }
    public NewUserEntity NewUser { get; set; }
}