﻿using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Core.Domain.ValueObjects;
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
                Contact = newUser.OptionalInfo.Contact,
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
            .Include(u => u.IdOAuthUserNavigation)
            .Select(u => new UserEntity
            {
                Id = u.Id,
                Nickname = u.Nickname,
                HashedPassword = u.HashedPassword,
                IsEmailConfirmed = !_authContext.TempUsers.Any(tu => tu.IdUser == u.Id),
                IsOAuthUser = u.IdOAuthUserNavigation != null,
                IsOAuthRegisterCompleted = u.IdOAuthUserNavigation != null && u.IdOAuthUserNavigation.OAuthRegisterConfirmationCode == null
            })
            .SingleOrDefaultAsync(ct);

        return appUser;
    }

    public async Task<UserSecurityEntity?> GetUserWithEmailsByIdAsync(int idUser, CancellationToken ct)
    {
        var appUser = await _authContext.Users
            .Where(u => u.Id == idUser)
            .Select(u => new UserSecurityEntity
            {
                Nickname = u.Nickname,
                HashedPassword = u.HashedPassword,
                Emails = u.Emails.Select(ue => new UserEmailSecurity
                {
                    Id = ue.Id,
                    IsMain = ue.IsMain,
                    Value = ue.Value
                }).ToList()
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
            Contact = userToConfirm.IdTempUserNavigation.Contact,
            City = userToConfirm.IdTempUserNavigation.City,
        };

        var role = await _appContext.Roles
            .Where(r => r.Name == AppRole.User)
            .SingleOrDefaultAsync(ct);

        var userRole = new UserRole
        {
            AppUserId = userToConfirm.Id,
            RoleIdNavigation = role
        };

        await using var tran = await _authContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _appContext.AppUsers.AddAsync(appUser, ct);
            await _appContext.UserRoles.AddAsync(userRole, ct);
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
        string confirmationToken, DateTime existsUntil, int? idUserEmail, CancellationToken ct)
    {
        var dbUser = await _authContext.Users
            .Include(u => u.ActionsToConfirm)
            .Include(u => u.IdTempUserNavigation)
            .SingleOrDefaultAsync(u => (u.Id == idUser) & (u.IdTempUserNavigation == null), ct);

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
                ActionType = actionType,
                ExistsUntil = existsUntil,
                IdUserEmail = idUserEmail
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
            .SingleOrDefaultAsync(u => u.Id == idUser && u.IdTempUserNavigation == null, ct);
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
            .Include(u => u.IdTempUserNavigation)
            .Select(u => new UserEntity
            {
                Id = u.Id,
                Nickname = u.Nickname,
                RefreshToken = u.RefreshToken,
                RefreshTokenExp = u.RefreshTokenExp,
                IsOAuthUser = u.IdOAuthUserNavigation != null
            })
            .SingleOrDefaultAsync(ct);
    }

    public async Task<UserEmailsEntity?> GetUserConfirmedEmailsAsync(int idUser, CancellationToken ct)
    {
        var userEmails = await _authContext.UsersEmails
            .Include(ue => ue.IdUserActionToConfirmNavigation)
            .Where(ue => ue.IdUser == idUser && ue.IdUserActionToConfirmNavigation == null).ToListAsync(ct);
        if (userEmails.Count == 0)
            return null;

        return new UserEmailsEntity()
        {
            MainEmail = userEmails.Where(ue => ue.IsMain).Select(ue => new UserEmailValue
            {
                IdEmail = ue.Id,
                Value = ue.Value
            }).SingleOrDefault(),
            ExtraEmails = userEmails.Where(ue => !ue.IsMain).Select(ue => new UserEmailValue
            {
                IdEmail = ue.Id,
                Value = ue.Value
            })
        };
    }

    public async Task<UserEmailToConfirmValue?> GetUserNotConfirmedEmailAsync(int idUser, CancellationToken ct)
    {
        return await _authContext.UsersEmails
            .Include(ue => ue.IdUserActionToConfirmNavigation)
            .Where(ue => ue.IdUser == idUser && ue.IdUserActionToConfirmNavigation != null)
            .Select(ue => new UserEmailToConfirmValue
            {
                IdEmail = ue.Id,
                Value = ue.Value,
                IsMainEmail = ue.IdUserActionToConfirmNavigation.ActionType == UserActionToConfirmEnum.NewMainEmail
            }).SingleOrDefaultAsync(ct);
    }

    public async Task<(bool isEmailConnected, int? idEmail)> GetExtraEmailIdAsync(int idUser, string email, CancellationToken ct)
    {
        var connectedEmail = await _authContext.UsersEmails.SingleOrDefaultAsync(ue => ue.IdUser == idUser && EF.Functions.ILike(ue.Value, email), ct);
        if (connectedEmail is null)
            return (isEmailConnected: false, idEmail: null);

        if (connectedEmail.IsMain)
            return (isEmailConnected: true, idEmail: null);
        return (isEmailConnected: true, idEmail: connectedEmail.Id);
    }

    public async Task<bool?> AddUserMainEmailToConfirmAsync(UserEmailToConfirm userEmailToConfirm, CancellationToken ct)
    {
        var userMainEmail = await _authContext.UsersEmails.SingleOrDefaultAsync(ue => ue.IdUser == userEmailToConfirm.IdUser && ue.IsMain, ct);
        if (userMainEmail is null)
            return null;

        if (userMainEmail.Value == userEmailToConfirm.Email)
            return true;

        await using var tran = await _authContext.Database.BeginTransactionAsync(ct);
        try
        {
            var userEmail = new UserEmail
            {
                IdUser = userEmailToConfirm.IdUser,
                IsMain = false,
                Value = userEmailToConfirm.Email
            };
            await _authContext.UsersEmails.AddAsync(userEmail, ct);
            if (await _authContext.SaveChangesAsync(ct) == 0)
            {
                await tran.RollbackAsync(ct);
                return false;
            }

            var status = await this.CreateUserActionToConfirmAsync(userEmailToConfirm.IdUser, UserActionToConfirmEnum.NewMainEmail, userEmailToConfirm.ConfirmationToken, userEmailToConfirm.ExistsUntil, userEmail.Id, ct);
            if (status == false)
            {
                await tran.RollbackAsync(ct);
                return false;
            }

            await tran.CommitAsync(ct);
            return true;

        }
        catch (Exception exc)
        {
            await tran.RollbackAsync(ct);
            if (exc.InnerException is OperationCanceledException)
                throw;
            return false;
        }
    }

    public async Task<bool?> UpdateUserMainEmailByExtraEmailAsync(UserEmailToConfirm userEmailToConfirm, CancellationToken ct)
    {
        var userMainEmail = await _authContext.UsersEmails.SingleOrDefaultAsync(ue => ue.IdUser == userEmailToConfirm.IdUser && ue.IsMain, ct);
        if (userMainEmail is null)
            return null;

        var userExtraEmail = await _authContext.UsersEmails.SingleOrDefaultAsync(ue => ue.IdUser == userEmailToConfirm.IdUser && ue.Id == userEmailToConfirm.IdEmail && !ue.IsMain, ct);
        if (userExtraEmail is null)
            return null;

        userMainEmail.IsMain = false;
        userExtraEmail.IsMain = true;
        if (await _authContext.SaveChangesAsync(ct) == 2)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> IsEmailAvailableAsync(int? idUser, string email, CancellationToken ct)
    {
        if (idUser is null)
            return !await _authContext.UsersEmails.AnyAsync(ue => EF.Functions.ILike(ue.Value, email), ct);
        return !await _authContext.UsersEmails.AnyAsync(ue => EF.Functions.ILike(ue.Value, email) && ue.IdUser != idUser, ct);
    }

    public async Task<bool?> AddExtraEmailAsync(UserEmailToConfirm userEmailToConfirm, CancellationToken ct)
    {
        var userExists = await _authContext.Users.AnyAsync(u => u.Id == userEmailToConfirm.IdUser, ct);
        if (!userExists)
            return null;

        var newUserEmail = new UserEmail()
        {
            IdUser = userEmailToConfirm.IdUser,
            IsMain = false,
            Value = userEmailToConfirm.Email
        };

        await using var tran = await _authContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _authContext.UsersEmails.AddAsync(newUserEmail, ct);
            if (await _authContext.SaveChangesAsync(ct) == 0)
            {
                await tran.RollbackAsync(ct);
                return false;
            }

            var status = await this.CreateUserActionToConfirmAsync(userEmailToConfirm.IdUser, UserActionToConfirmEnum.NewExtraEmail, userEmailToConfirm.ConfirmationToken, userEmailToConfirm.ExistsUntil, newUserEmail.Id, ct);
            if (status == true)
            {
                await tran.CommitAsync(ct);
            }
            else
            {
                await tran.RollbackAsync(ct);
            }

            return status;
        }
        catch (Exception exc)
        {
            await tran.RollbackAsync(ct);
            if (exc.InnerException is OperationCanceledException)
                throw;
            return false;
        }
    }

    public async Task<bool?> UpdateUserPasswordAsync(int idUser, string newHashedPassword, CancellationToken ct)
    {
        var user = await _authContext.Users.SingleOrDefaultAsync(u => u.Id == idUser, ct);
        if (user is null)
            return null;

        user.HashedPassword = newHashedPassword;
        return !(await _authContext.SaveChangesAsync(ct) == 0);
    }

    public async Task<(bool? isSuccess, string? deletedEmail)> DeleteExtraEmailAsync(int idUser, int idExtraEmail, CancellationToken ct)
    {
        var userEmail = await _authContext.UsersEmails
            .Include(ue => ue.IdUserActionToConfirmNavigation)
            .SingleOrDefaultAsync(ue => ue.IdUser == idUser && ue.Id == idExtraEmail && !ue.IsMain, ct);

        if (userEmail is null)
            return (isSuccess: null, deletedEmail: null);
        if (userEmail.IdUserActionToConfirmNavigation != null)
            return (isSuccess: false, deletedEmail: null);

        _authContext.UsersEmails.Remove(userEmail);
        return (isSuccess: !(await _authContext.SaveChangesAsync(ct) == 0), deletedEmail: userEmail.Value);
    }

    public async Task<CheckOptionOfAddNewExtraEmail> IsUserAllowedToAddExtraEmailAsync(int idUser, CancellationToken ct)
    {
        var userEmails = await _authContext.UsersEmails
            .Include(ue => ue.IdUserActionToConfirmNavigation).Where(ue => ue.IdUser == idUser).ToListAsync(ct);
        if (userEmails.Count == 0)
            return CheckOptionOfAddNewExtraEmail.UserNotExist;
        else if (userEmails.Where(ue => !ue.IsMain).ToList().Count == 3)
            return CheckOptionOfAddNewExtraEmail.OverLimitOfExtraEmails;
        else if (userEmails.Any(ue => ue.IdUserActionToConfirmNavigation != null))
            return CheckOptionOfAddNewExtraEmail.UserHasActionToConfirm;
        return CheckOptionOfAddNewExtraEmail.AllowedToAdd;
    }

    public async Task<(bool isSuccess, bool isMainEmail)> ConfirmUserEmailAsync(string email, string confirmationCode, CancellationToken ct)
    {
        var userEmail = await _authContext.UsersEmails.SingleOrDefaultAsync(ue => ue.Value == email, ct);
        if (userEmail is null)
            return (isSuccess: false, isMainEmail: false);

        var userEmailActionToConfirm = await _authContext.UsersActionsToConfirm.SingleOrDefaultAsync(u => u.IdUserEmail == userEmail.Id && u.ConfirmationToken == confirmationCode, ct);
        if (userEmailActionToConfirm is null || userEmailActionToConfirm.ExistsUntil < new DateTime())
            return (isSuccess: false, isMainEmail: false);

        if (userEmailActionToConfirm.ActionType == UserActionToConfirmEnum.NewMainEmail)
        {
            var userMainEmail = await _authContext.UsersEmails.SingleOrDefaultAsync(ue => ue.IdUser == userEmailActionToConfirm.IdUser && ue.IsMain, ct);
            if (userMainEmail is null)
                return (isSuccess: false, isMainEmail: false);

            userEmail.IsMain = true;
            _authContext.UsersActionsToConfirm.Remove(userEmailActionToConfirm);
            _authContext.UsersEmails.Remove(userMainEmail);

            if (await _authContext.SaveChangesAsync(ct) == 3)
            {
                return (isSuccess: true, isMainEmail: true);
            }

            return (isSuccess: false, isMainEmail: false);
        }
        else if (userEmailActionToConfirm.ActionType == UserActionToConfirmEnum.NewExtraEmail)
        {
            _authContext.UsersActionsToConfirm.Remove(userEmailActionToConfirm);
            if (await _authContext.SaveChangesAsync(ct) == 0)
                return (isSuccess: false, isMainEmail: false);
            return (isSuccess: true, isMainEmail: false);
        }
        return (isSuccess: false, isMainEmail: false);
    }

    public async Task<bool> UpdateActionToConfirmEmailAsync(UserEmailToConfirm userEmailToConfirm, CancellationToken ct)
    {
        var emailToConfirm = await _authContext.UsersActionsToConfirm.SingleOrDefaultAsync(u => u.IdUser == userEmailToConfirm.IdUser && u.IdUserEmail == userEmailToConfirm.IdEmail, ct);
        if (emailToConfirm is null)
            return false;
        emailToConfirm.ConfirmationToken = userEmailToConfirm.ConfirmationToken;
        emailToConfirm.ExistsUntil = userEmailToConfirm.ExistsUntil;
        return !(await _authContext.SaveChangesAsync(ct) == 0);
    }

    public async Task<bool> CancelEmailConfirmationActionAsync(int idUser, CancellationToken ct)
    {
        var action = await _authContext.UsersActionsToConfirm.SingleOrDefaultAsync(u => u.IdUser == idUser && (u.ActionType == UserActionToConfirmEnum.NewMainEmail || u.ActionType == UserActionToConfirmEnum.NewExtraEmail), ct);
        if (action is null)
            return false;

        var userEmail = await _authContext.UsersEmails.SingleOrDefaultAsync(ue => ue.Id == action.IdUserEmail, ct);
        if (userEmail is null)
            return false;

        _authContext.UsersActionsToConfirm.Remove(action);
        _authContext.UsersEmails.Remove(userEmail);

        if (await _authContext.SaveChangesAsync(ct) == 2)
        {
            return true;
        }

        return false;
    }

    public async Task<(int? idEmail, UserActionToConfirmEnum? actionType)> GetEmailToConfirmAsync(int idUser, CancellationToken ct)
    {
        var emailToConfirm = await _authContext.UsersActionsToConfirm
            .SingleOrDefaultAsync(u => u.IdUser == idUser && (u.ActionType == UserActionToConfirmEnum.NewMainEmail || u.ActionType == UserActionToConfirmEnum.NewExtraEmail), ct);
        return (emailToConfirm?.IdUserEmail, emailToConfirm?.ActionType);
    }

    public async Task<string?> GetMainEmailByEmailToConfirmAsync(string email, CancellationToken ct)
    {
        var user = await _authContext.Users
            .Include(ue => ue.Emails)
            .SingleOrDefaultAsync(u => u.Emails.Any(e => EF.Functions.Like(e.Value, email)), ct);

        return user is null ? null : user.Emails.SingleOrDefault(e => e.IsMain)!.Value;
    }

    public async Task<bool> RegisterOAuthUserAsync(string oAuthId, string oAuthEmail, string oAuthCode, OAuthProviderEnum provider, CancellationToken ct)
    {
        var newAuthUser = new User
        {
            Nickname = null,
            HashedPassword = "",
            IdOAuthUserNavigation = new OAuthUser
            {
                OAuthId = oAuthId,
                OAuthProvider = provider,
                OAuthRegisterConfirmationCode = oAuthCode
            },
            Emails = new List<UserEmail>
            {
                new() { IsMain = true, Value = oAuthEmail }
            }
        };

        using var tran = await _authContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _authContext.Users.AddAsync(newAuthUser, ct);
            var isAdded = await _authContext.SaveChangesAsync(ct) == 3;
            if (!isAdded) return false;

            var newAppUser = new AppUser
            {
                Id = newAuthUser.Id,
                Nickname = newAuthUser.Nickname
            };

            var role = await _appContext.Roles.SingleOrDefaultAsync(r => r.Name == AppRole.User, ct);
            var userRole = new UserRole()
            {
                RoleIdNavigation = role,
                AppUserId = newAppUser.Id
            };

            await _appContext.AppUsers.AddAsync(newAppUser, ct);
            await _appContext.UserRoles.AddAsync(userRole, ct);
            isAdded = await _appContext.SaveChangesAsync(ct) >= 2;
            if (!isAdded)
            {
                await tran.RollbackAsync(ct);
                return false;
            }

            await tran.CommitAsync(ct);
            return true;
        }
        catch
        {
            await tran.RollbackAsync(ct);
            return false;
        }
    }

    public async Task UpdateOAuthUserRegisterConfirmationCodeAsync(int userId, string oAuthCode, CancellationToken ct)
    {
        var updatedOAuthUser = new OAuthUser
        {
            IdUser = userId,
            OAuthRegisterConfirmationCode = oAuthCode
        };

        _authContext.Entry(updatedOAuthUser).Property(ou => ou.OAuthRegisterConfirmationCode).IsModified = true;
        await _authContext.SaveChangesAsync(ct);
    }

    public async Task<int?> ConfirmOAuthRegisterAsync(string confirmationCode, NewUserEntity newUser, CancellationToken ct)
    {
        var oAuthUser = await _authContext.OAuthUsers
            .Include(ou => ou.IdUserNavigation)
            .Where(ou => ou.OAuthRegisterConfirmationCode == confirmationCode)
            .SingleOrDefaultAsync(ct);

        if (oAuthUser is null) return null;

        using var tran = await _authContext.Database.BeginTransactionAsync(ct);
        try
        {
            oAuthUser.OAuthRegisterConfirmationCode = null;
            oAuthUser.IdUserNavigation.Nickname = newUser.Nickname;

            var success = await _authContext.SaveChangesAsync(ct) == 2;
            if (!success) return null;

            var appUser = await _appContext.AppUsers.Where(u => u.Id == oAuthUser.IdUser).SingleOrDefaultAsync(ct);
            if (appUser is null)
            {
                await tran.RollbackAsync(ct);
                return null;
            }

            appUser.Nickname = newUser.Nickname;
            appUser.FirstName = newUser.OptionalInfo.FirstName;
            appUser.LastName = newUser.OptionalInfo.LastName;
            appUser.Birthdate = newUser.OptionalInfo.Birthdate;
            appUser.City = newUser.OptionalInfo.City;
            appUser.Contact = newUser.OptionalInfo.Contact;

            success = await _appContext.SaveChangesAsync(ct) == 1;
            if (!success)
            {
                await tran.RollbackAsync(ct);
                return null;
            }
            await tran.CommitAsync(ct);
            return appUser.Id;
        }
        catch
        {
            await tran.RollbackAsync(ct);
            return null;
        }
    }

    public async Task<int> GetUserIdByEmailAsync(string email, CancellationToken ct)
    {
        return await _authContext.UsersEmails.Where(ue => EF.Functions.Like(ue.Value, email)).Select(ue => ue.IdUser).SingleOrDefaultAsync(ct);
    }
}