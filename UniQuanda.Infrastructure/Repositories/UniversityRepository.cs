using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using UniQuanda.Infrastructure.Presistence.AuthDb;

namespace UniQuanda.Infrastructure.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly AppDbContext _context;
        private readonly AuthDbContext _authDbContext;
        public UniversityRepository(AppDbContext context, AuthDbContext authDbContext)
        {
            _context = context;
            _authDbContext = authDbContext;
        }
        public async Task<bool> AddUserToUniversityAsync(int userId, int universityId, CancellationToken ct)
        {
            if (await _context.AppUsersInUniversities.AnyAsync(au => au.UniversityId == universityId && au.AppUserId == userId, ct))
                return false;
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
                .Where(u => !u.AppUsersInUniversity.Any(uu => uu.AppUserId == uid))
                .Select(u => new UniversityEntity()
                {
                    Id = u.Id,
                    Regex = u.Regex
                })
                .ToListAsync(ct);
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

        public async Task RemoveUserFromUniversityByEmailAsync(int userId, string email, CancellationToken ct)
        {
            var universities = await _context.Universities.ToListAsync(ct);
            University? emailUniversity = null;
            foreach (var university in universities)
            {
                var regex = new Regex(@university.Regex, RegexOptions.IgnoreCase);
                if (regex.IsMatch(email))
                {
                    emailUniversity = university;
                }
            }
            if (emailUniversity == null)
                return;

            var userEmails = await _authDbContext.UsersEmails.Where(ue => ue.IdUser == userId).ToListAsync(ct);
            int emailCount = 0;
            foreach (var userEmail in userEmails)
            {
                var regex = new Regex(@emailUniversity.Regex, RegexOptions.IgnoreCase);
                if (regex.IsMatch(userEmail.Value))
                {
                    emailCount += 1;
                }
            }

            if (emailCount == 0)
            {
                var universityToRemove = await _context.AppUsersInUniversities.SingleOrDefaultAsync(a => a.UniversityId == emailUniversity.Id && a.AppUserId == userId, ct);
                _context.AppUsersInUniversities.Remove(universityToRemove);
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}
