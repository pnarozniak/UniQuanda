using MediatR;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Register;

public class RegisterCommand : IRequest<bool>
{
    public RegisterCommand(RegisterRequestDTO request)
    {
        PlainPassword = request.Password;

        NewUser = new NewUserEntity
        {
            Nickname = request.Nickname,
            Email = request.Email,
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

    public NewUserEntity NewUser { get; set; }
    public string PlainPassword { get; set; }
}