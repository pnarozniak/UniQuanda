using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Infrastructure.Presistence.AuthDb;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Repositories;

public class SecurityRepository : ISecurityRepository
{
    private readonly AuthDbContext _authContext;

    public SecurityRepository(AuthDbContext authDbContext)
    {
        _authContext = authDbContext;
    }

    public async Task<UserEmailsEntity?> GetUserEmailsAsync(int idUser, CancellationToken ct)
    {
        var userEmails = await _authContext.UsersEmails.Where(ue => ue.IdUser == idUser).ToListAsync(ct);
        if (userEmails.Count == 0)
            return null;

        return new UserEmailsEntity()
        {
            MainEmail = userEmails.SingleOrDefault(ue => ue.IsMain == true).Value,
            ExtraEmails = userEmails.Where(ue => !ue.IsMain).Select(ue => ue.Value)
        };
    }

    public async Task<string?> GetUserHashedPasswordByIdAsync(int idUser, CancellationToken ct)
    {
        var hashedPassword = await _authContext.Users
            .Where(u => u.Id == idUser)
            .Select(u => u.HashedPassword)
            .SingleOrDefaultAsync(ct);

        return hashedPassword;
    }
    public async Task<bool> IsEmailConnectedWithUserAsync(int idUser, string email, CancellationToken ct)
    {
        return await _authContext.UsersEmails.AnyAsync(ue => ue.IdUser == idUser && EF.Functions.ILike(ue.Value, email), ct);
    }

    public async Task<bool?> UpdateUserMainEmailAsync(int idUser, string newMainEmail, CancellationToken ct)
    {
        var userMainEmail = await _authContext.UsersEmails.SingleOrDefaultAsync(ue => ue.IdUser == idUser && ue.IsMain, ct);
        if (userMainEmail is null)
            return null;

        if (userMainEmail.Value == newMainEmail)
            return true;

        var userExtraEmail = await _authContext.UsersEmails.SingleOrDefaultAsync(ue => ue.IdUser == idUser && EF.Functions.ILike(ue.Value, newMainEmail) && !ue.IsMain, ct);
        if (userExtraEmail is null)
            return null;

        await using var tran = await _authContext.Database.BeginTransactionAsync(ct);
        try
        {
            userMainEmail.IsMain = false;
            userExtraEmail.IsMain = true;
            if (await _authContext.SaveChangesAsync(ct) == 2)
            {
                await tran.CommitAsync(ct);
                return true;
            }

            await tran.RollbackAsync(ct);
            return false;
        }
        catch (Exception exc)
        {
            await tran.RollbackAsync(ct);
            if (exc.InnerException is OperationCanceledException)
                throw;
            return false;
        }
    }

    public async Task<bool> IsEmailAvailableAsync(string email, CancellationToken ct)
    {
        return !await _authContext.UsersEmails.AnyAsync(ue => EF.Functions.ILike(ue.Value, email), ct);
    }

    public async Task<bool?> AddExtraEmailAsync(int idUser, string newExtraEmail, CancellationToken ct)
    {
        var numberOfExtraEmails = await _authContext.UsersEmails.Where(ue => ue.IdUser == idUser && !ue.IsMain).CountAsync(ct);
        if (numberOfExtraEmails == 3)
            return null;

        var newUserEmail = new UserEmail()
        {
            IdUser = idUser,
            IsMain = false,
            Value = newExtraEmail
        };
        await _authContext.UsersEmails.AddAsync(newUserEmail, ct);
        if (await _authContext.SaveChangesAsync(ct) == 0)
            return false;
        return true;
    }
}