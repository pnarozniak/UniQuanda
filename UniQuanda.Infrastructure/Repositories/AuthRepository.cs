using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities;
using UniQuanda.Core.Domain.ValueObjects;
using UniQuanda.Infrastructure.Presistence.AuthDb;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;
using UserEmail = UniQuanda.Infrastructure.Presistence.AuthDb.Models.UserEmail;

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
                .AnyAsync(u => u.Emails.Any(ue => EF.Functions.ILike(ue.Value, email)));
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
                    EmailConfirmationCode = newUser.EmailConfirmationToken,
                    Birthdate = newUser.OptionalInfo.Birthdate,
                    FirstName = newUser.OptionalInfo.FirstName,
                    LastName = newUser.OptionalInfo.LastName,
                    PhoneNumber = newUser.OptionalInfo.PhoneNumber,
                    City = newUser.OptionalInfo.City,
                    ExistsTo = newUser.ExistsTo
                },
                Emails = new List<UserEmail>()
                {
                    new()
                    {
                        IsMain = true,
                        Value = newUser.Email
                    }
                }
            };

            try
            {
                await _authContext.Users.AddAsync(userToRegister);
                return await _authContext.SaveChangesAsync() >= 3;
            }
            catch
            {
                return false;
            }
        }

        public async Task<AppUser?> GetUserByEmailAsync(string email)
        {
            var appUser = await _authContext.Users
                .Where(u => u.Emails.Any(ue => EF.Functions.ILike(ue.Value, email)))
                .Select(u => new AppUser()
                {
                    Id = u.Id,
                    Nickname = u.Nickname,
                    HashedPassword = u.HashedPassword,
                    OptionalInfo = new UserOptionalInfo()
                    {
                        //TODO This is mocked part of the code, should be replaced in the future
                        Avatar = _authContext.TempUsers.Any(tu => tu.IdUser == u.Id) ? null : $"https://robohash.org/{u.Nickname}"
                    },
                    IsEmailConfirmed = !_authContext.TempUsers.Any(tu => tu.IdUser == u.Id)
                })
                .SingleOrDefaultAsync();

            return appUser;
        }

        public async Task<bool?> UpdateUserRefreshTokenAsync(int idUser, string refreshToken, DateTime refreshTokenExp)
        {
            var user = await _authContext.Users.SingleOrDefaultAsync(u => u.Id == idUser);
            if (user is null)
                return null;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExp = refreshTokenExp;
            return await _authContext.SaveChangesAsync() >= 1;
        }
    }
}
