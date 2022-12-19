using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly AppDbContext _context;
        public UniversityRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddUserToUniversityAsync(int userId, int universityId, CancellationToken ct)
        {
            var order = await _context.AppUsersInUniversities.Where(uu => uu.AppUserId == userId)
                .OrderByDescending(uu => uu.Order)
                .Select(uu => uu.Order).SingleOrDefaultAsync(ct);
            order = order == default(int) ? 1 : order + 1;
            var newUserUniversityConnection = new AppUserInUniversity()
            {
                AppUserId = userId,
                UniversityId = universityId,
                Order = order
            };
            await _context.AppUsersInUniversities.AddAsync(newUserUniversityConnection, ct);
            var result = await _context.SaveChangesAsync(ct);
            return result != 0;
        }

        public async Task<IEnumerable<UniversityEntity>> GetUniversitiresWhereUserIsNotPresentAsync(int uid, CancellationToken ct)
        {
            return await _context.Universities
            .GroupJoin(
                _context.AppUsersInUniversities,
                u => u.Id,
                a => a.UniversityId,
                (u, joined) => new { University = u, Joined = joined }
            )
            .SelectMany(
                x => x.Joined.DefaultIfEmpty(),
                (x, j) => new { University = x.University, AppUserInUniversity = j })
            .Where(x => x.AppUserInUniversity == null || x.AppUserInUniversity.AppUserId != uid)
            .Select(x => new UniversityEntity()
            {
                Id = x.University.Id,
                Regex = x.University.Regex,
            }).ToListAsync(ct);
        }

        public async Task<UniversityEntity?> GetUniversityByIdAsync(int universityId, CancellationToken ct)
        {
            var university = await _context.Universities.FindAsync(universityId);
            if (university == null)
            {
                return null;
            }
            return new UniversityEntity()
            {
                Id = university.Id,
                Logo = university.Logo,
                Contact = university.Contact,
                Name = university.Name,
            };
        }
    }
}
