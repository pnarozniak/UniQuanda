using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities;
using UniQuanda.Infrastructure.Presistence.AuthDb;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _authContext;
        public AuthRepository(AuthDbContext authContext)
        {
            _authContext = authContext;
        }
        public async Task<bool> IsEmailUsedAsync(string email)
        {
            return await _authContext.Users
                .AnyAsync(u => 
                    EF.Functions.ILike(u.IdTempUserNavigation.Email, email) ||
                    u.Emails.Any(ue => EF.Functions.ILike(ue.Value, email)));
        }

        public async Task<bool> IsNicknameUsedAsync(string nickname)
        {
            return await _authContext.Users
                .AnyAsync(u => EF.Functions.ILike(u.Nickname, nickname));
        }

        public async Task<bool> RegisterNewUserAsync(NewUser newUser)
        {
            var userToRegister = new User
            {
                Nickname = newUser.Nickname,
                HashedPassword = newUser.HashedPassword,
                IdTempUserNavigation = new TempUser()
                {
                    Email = newUser.Email,
                    EmailConfirmationCode = newUser.EmailConfirmationToken,
                    Birthdate = newUser.OptionalInfo.Birthdate,
                    FirstName = newUser.OptionalInfo.FirstName,
                    LastName = newUser.OptionalInfo.LastName,
                    PhoneNumber = newUser.OptionalInfo.PhoneNumber,
                    City = newUser.OptionalInfo.City,
                    ExistsTo = newUser.ExistsTo
                }
            };

            try
            {
                await _authContext.Users.AddAsync(userToRegister);
                return await _authContext.SaveChangesAsync() >= 2;
            }
            catch
            {
                return false;
            }
        }
    }
}
