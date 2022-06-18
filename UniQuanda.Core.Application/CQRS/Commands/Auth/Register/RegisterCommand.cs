using MediatR;
using UniQuanda.Core.Domain.Entities;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Register
{
    public class RegisterCommand : IRequest<bool>
    {
        public NewUser NewUser { get; set; }
        public string PlainPassword { get; set; }

        public RegisterCommand(RegisterRequestDTO request)
        {
            this.PlainPassword = request.Password;
            
            this.NewUser = new NewUser()
            {
                Nickname = request.Nickname,
                Email = request.Email,
                OptionalInfo = new UserOptionalInfo()
                {
                    Birthdate = request.Birthdate,
                    City = request.City,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber
                }
            };
        }
    }
}
