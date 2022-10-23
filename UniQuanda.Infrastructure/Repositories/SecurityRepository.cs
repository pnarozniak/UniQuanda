using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Infrastructure.Presistence.AuthDb;

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
}