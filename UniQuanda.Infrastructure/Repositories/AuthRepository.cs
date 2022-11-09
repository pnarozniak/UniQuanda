using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using UniQuanda.Infrastructure.Presistence.AuthDb;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _appContext;
    private readonly AuthDbContext _authContext;

    public AuthRepository(AuthDbContext authContext, AppDbContext appContext)
    {
        _authContext = authContext;
        _appContext = appContext;
    }

    public async Task<bool> IsEmailUsedAsync(string email, CancellationToken ct)
    {
        return await _authContext.Users
            .AnyAsync(u => u.Emails.Any(ue => EF.Functions.ILike(ue.Value, email)), ct);
    }

    public async Task<bool> IsNicknameUsedAsync(string nickname, CancellationToken ct)
    {
        return await _authContext.Users
            .AnyAsync(u => EF.Functions.ILike(u.Nickname, nickname), ct);
    }

    public async Task<bool> RegisterNewUserAsync(NewUserEntity newUser, CancellationToken ct)
    {
        var userToRegister = new User
        {
            Nickname = newUser.Nickname,
            HashedPassword = newUser.HashedPassword,
            IdTempUserNavigation = new TempUser
            {
                EmailConfirmationCode = newUser.EmailConfirmationToken,
                Birthdate = newUser.OptionalInfo.Birthdate,
                FirstName = newUser.OptionalInfo.FirstName,
                LastName = newUser.OptionalInfo.LastName,
                PhoneNumber = newUser.OptionalInfo.PhoneNumber,
                City = newUser.OptionalInfo.City,
                ExistsUntil = newUser.ExistsUntil
            },
            Emails = new List<UserEmail>
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
            await _authContext.Users.AddAsync(userToRegister, ct);
            return await _authContext.SaveChangesAsync(ct) == 3;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch
        {
            return false;
        }
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken ct)
    {
        var appUser = await _authContext.Users
            .Where(u => u.Emails.Any(ue => EF.Functions.ILike(ue.Value, email)))
            .Select(u => new UserEntity
            {
                Id = u.Id,
                Nickname = u.Nickname,
                HashedPassword = u.HashedPassword,
                IsEmailConfirmed = !_authContext.TempUsers.Any(tu => tu.IdUser == u.Id)
            })
            .SingleOrDefaultAsync(ct);

        return appUser;
    }

    public async Task<bool?> UpdateUserRefreshTokenAsync(int idUser, string refreshToken, DateTime refreshTokenExp,
        CancellationToken ct)
    {
        var user = await _authContext.Users.SingleOrDefaultAsync(u => u.Id == idUser, ct);
        if (user is null)
            return null;

        user.RefreshToken = refreshToken;
        user.RefreshTokenExp = refreshTokenExp;
        return await _authContext.SaveChangesAsync(ct) >= 1;
    }

    public async Task<bool> ConfirmUserRegistrationAsync(string email, string confirmationCode, CancellationToken ct)
    {
        var userToConfirm = await _authContext.Users
            .Include(u => u.IdTempUserNavigation)
            .Where(u => EF.Functions.Like(u.IdTempUserNavigation.EmailConfirmationCode, confirmationCode))
            .Where(u => EF.Functions.ILike(u.Emails.Select(ue => ue.Value).First(), email))
            .SingleOrDefaultAsync(ct);

        if (userToConfirm is null) return false;

        var appUser = new AppUser
        {
            Id = userToConfirm.Id,
            Nickname = userToConfirm.Nickname,
            FirstName = userToConfirm.IdTempUserNavigation.FirstName,
            LastName = userToConfirm.IdTempUserNavigation.LastName,
            Birthdate = userToConfirm.IdTempUserNavigation.Birthdate,
            PhoneNumber = userToConfirm.IdTempUserNavigation.PhoneNumber,
            City = userToConfirm.IdTempUserNavigation.City
        };

        await using var tran = await _authContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _appContext.AppUsers.AddAsync(appUser, ct);
            var isAdded = await _appContext.SaveChangesAsync(ct) >= 1;
            if (!isAdded) return false;

            _authContext.TempUsers.Remove(userToConfirm.IdTempUserNavigation);
            await _authContext.SaveChangesAsync(ct);

            await tran.CommitAsync(ct);
            return true;
        }
        catch (Exception exc)
        {
            await tran.RollbackAsync(ct);
            if (exc.InnerException is OperationCanceledException) throw;
            return false;
        }
    }

    public async Task<bool?> UpdateTempUserEmailConfirmationCodeAsync(string email, string confirmationCode,
        CancellationToken ct)
    {
        var tempUser = await _authContext.TempUsers
            .Where(tu => EF.Functions.ILike(tu.IdUserNavigation.Emails.Select(e => e.Value).First(), email))
            .SingleOrDefaultAsync(ct);

        if (tempUser is null) return null;

        tempUser.EmailConfirmationCode = confirmationCode;
        return await _authContext.SaveChangesAsync(ct) >= 1;
    }

    public async Task<bool?> CreateUserActionToConfirmAsync(int idUser, UserActionToConfirmEnum actionType,
        string confirmationToken, DateTime existsUntil, CancellationToken ct)
    {
        var dbUser = await _authContext.Users
            .Include(u => u.ActionsToConfirm)
            .Include(u => u.IdTempUserNavigation)
            .SingleOrDefaultAsync(u => (u.Id == idUser) & (u.IdTempUserNavigation == null));

        if (dbUser is null) return null;

        var existingAction = dbUser.ActionsToConfirm.SingleOrDefault(a => a.ActionType == actionType);
        if (existingAction is not null)
        {
            existingAction.ConfirmationToken = confirmationToken;
            existingAction.ExistsUntil = existsUntil;
        }
        else
        {
            var actionConfirmation = new UserActionToConfirm
            {
                IdUserNavigation = dbUser,
                ConfirmationToken = confirmationToken,
                ExistsUntil = existsUntil
            };
            await _authContext.UsersActionsToConfirm.AddAsync(actionConfirmation, ct);
        }

        return await _authContext.SaveChangesAsync(ct) == 1;
    }

    public async Task<UserActionToConfirmEntity?> GetUserActionToConfirmAsync(UserActionToConfirmEnum actionType,
        string confirmationToken, CancellationToken ct)
    {
        return await _authContext.UsersActionsToConfirm
            .Where(ua => ua.ActionType == actionType && EF.Functions.Like(ua.ConfirmationToken, confirmationToken))
            .Select(ua => new UserActionToConfirmEntity
            {
                Id = ua.Id,
                IdUser = ua.IdUser,
                ConfirmationToken = ua.ConfirmationToken,
                ExistsUntil = ua.ExistsUntil,
                ActionType = ua.ActionType
            })
            .SingleOrDefaultAsync(ct);
    }

    public async Task<bool> ResetUserPasswordAsync(int idUser, int idRecoveryAction, string newHashedPassword,
        CancellationToken ct)
    {
        var dbUser = await _authContext.Users
            .Include(u => u.IdTempUserNavigation)
            .SingleOrDefaultAsync(u => u.Id == idUser && u.IdTempUserNavigation == null);
        if (dbUser is null) return false;

        dbUser.HashedPassword = newHashedPassword;
        dbUser.RefreshToken = null;
        dbUser.RefreshTokenExp = null;

        var action = new UserActionToConfirm { Id = idRecoveryAction };
        _authContext.UsersActionsToConfirm.Attach(action);
        _authContext.UsersActionsToConfirm.Remove(action);

        return await _authContext.SaveChangesAsync(ct) == 2;
    }

    public async Task<UserEntity?> GetUserByIdAsync(int idUser, CancellationToken ct)
    {
        return await _authContext.Users
            .Where(u => u.Id == idUser)
            .Select(u => new UserEntity
            {
                Id = u.Id,
                RefreshToken = u.RefreshToken,
                RefreshTokenExp = u.RefreshTokenExp
            })
            .SingleOrDefaultAsync(ct);
    }
}