using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
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

        public async Task<bool> AddExecutionOfPermissionToUserAsync(int userId, string permission, CancellationToken ct)
        {
            var usage = await _context.PermissionUsageByUsers
                .Where(pu => pu.AppUserId == userId && pu.PermissionIdNavigation.Name == permission)
                .FirstOrDefaultAsync(ct);

            usage.UsedTimes += 1;
            return await _context.SaveChangesAsync(ct) >= 1;
        }

        public async Task<bool> AssignAppRoleToUserAsync(int userId, AppRole role, DateTime? validUntil, CancellationToken ct)
        {
            var roleDb = await _context.Roles.Where(r => r.Name == role.Value).SingleOrDefaultAsync(ct);
            if (roleDb == null) return false;

            var previousRole = await _context.UserRoles.Where(ur => ur.AppUserId == userId && ur.RoleId == roleDb.Id).SingleOrDefaultAsync(ct);
            if (previousRole == null)
            {
                var userRole = new UserRole()
                {
                    AppUserId = userId,
                    RoleId = roleDb.Id,
                    ValidUnitl = validUntil
                };
                await _context.UserRoles.AddAsync(userRole, ct);
            }
            else
            {
                previousRole.ValidUnitl = validUntil;
                _context.UserRoles.Update(previousRole);
            }

            return await _context.SaveChangesAsync(ct) != 0;
        }

        public async Task<(int? maxAmount, int? usedAmount, DurationEnum? closestClearInterval)>
            GetExecutesOfPermissionByUserAsync(int userId, string permission, CancellationToken ct)
        {
            var rolesSettings = await _context.RolePermissions
                .Join(_context.UserRoles, rp => rp.RoleId, ur => ur.RoleId, (rp, ur) => new { rp, ur })
                .Where(x => x.rp.PermissionIdNavigation.Name == permission && x.ur.AppUserId == userId)
                .Select(x => new { maxAmount = x.rp.AllowedUsages, refreshPeriod = x.rp.LimitRefreshPeriod })
                .ToListAsync(ct);

            if (rolesSettings.Count == 0)
                return (0, 0, null);
            if (rolesSettings.Any(rs => rs.maxAmount == null))
                return (null, null, null);

            var closestClearInterval = rolesSettings.OrderBy(rs => rs.refreshPeriod).First().refreshPeriod;
            var maxAmount = rolesSettings.OrderByDescending(rs => rs.maxAmount).First().maxAmount;
            var usage = await _context.PermissionUsageByUsers.Where(pu => pu.PermissionIdNavigation.Name == permission && pu.AppUserId == userId).SingleOrDefaultAsync(ct);
            if (usage == null)
            {
                usage = new PermissionUsageByUser()
                {
                    UsedTimes = 0,
                    AppUserId = userId,
                    PermissionIdNavigation = await _context.Permissions.Where(p => p.Name == permission).FirstAsync(ct)
                };
                await _context.AddAsync(usage, ct);
                await _context.SaveChangesAsync();
            }
            var usedAmount = usage.UsedTimes;
            return (maxAmount, usedAmount, (DurationEnum)closestClearInterval);
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
