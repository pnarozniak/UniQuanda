using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AssignAppRoleToUserAsync(int userId, AppRole role, DateTime? validUntil, CancellationToken ct)
        {
            var roleDb = await _context.Roles.Where(r => r.Name == role.Value).SingleOrDefaultAsync(ct);
            if (roleDb == null) return false;

            roleDb.UserRoles.Add(new UserRole()
            {
                AppUserId = userId,
                ValidUnitl = validUntil
            });

            return await _context.SaveChangesAsync(ct) != 0;
        }

        public async Task<IEnumerable<AppRoleEntity>> GetNotExpiredUserRolesAsync(int userId, CancellationToken ct)
        {
            return await _context.UserRoles
                .Where(ur => ur.AppUserId == userId && (ur.ValidUnitl > DateTime.UtcNow || ur.ValidUnitl == null))
                .Select(ur => new AppRoleEntity()
                {
                    Id = ur.RoleId,
                    ValidUntil = ur.ValidUnitl,
                    Name = new AppRole()
                    {
                        Value = ur.RoleIdNavigation.Name
                    },
                })
                .ToListAsync(ct);
        }
    }
}
