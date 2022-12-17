using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Repositories
{
    public class AcademicTitleRepository : IAcademicTitleRepository
    {
        private readonly AppDbContext _context;
        public AcademicTitleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAcademicTitleRequestForUserAsync(int id, int uid, string imageUrl, int titleId, string? additionalInfo, DateTime createdAt, CancellationToken ct)
        {
            var tran = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                var image = new Image
                {
                    URL = imageUrl
                };
                await _context.Images.AddAsync(image, ct);

                var request = new TitleRequest()
                {
                    Id = id,
                    ScanIdNavigation = image,
                    AcademicTitleId = titleId,
                    AppUserId = uid,
                    AdditionalInfo = additionalInfo,
                    CreatedAt = createdAt,
                    TitleRequestStatus = Core.Domain.Enums.TitleRequestStatusEnum.Pending
                };
                await _context.TitleRequests.AddAsync(request, ct);

                await _context.SaveChangesAsync(ct);
                await tran.CommitAsync(ct);
                return true;
            }
            catch
            {
                await tran.RollbackAsync(ct);
                return false;
            }
        }

        public async Task<IEnumerable<AcademicTitleEntity>> GetAcademicTitlesOfUserAsync(int uid, CancellationToken ct)
        {
            return await _context.AppUsersTitles.Where(ut => ut.AppUserId == uid)
                .Select(ut => new AcademicTitleEntity
                {
                    Id = ut.AcademicTitleId,
                    Name = ut.AcademicTitleIdNavigation.Name,
                    Order = ut.Order,
                    Type = ut.AcademicTitleIdNavigation.AcademicTitleType
                }).ToListAsync(ct);
        }

        public async Task<int> GetNextTitleRequestIdAsync(CancellationToken ct)
        {
            return (await _context.IntFunctionWrapper
                .FromSqlRaw("SELECT nextval('\"TitleRequests_Id_seq\"') AS result")
                .FirstAsync(ct)
                ).result;
        }

        public async Task<IEnumerable<AcademicTitleEntity>> GetRequestableAcademicTitlesAsync(CancellationToken ct)
        {
            return await _context.AcademicTitles.Where(t => t.AcademicTitleType != Core.Domain.Enums.AcademicTitleEnum.Academic)
                .Select(t => new AcademicTitleEntity
                {
                    Id = t.Id,
                    Name = t.Name,
                    Type = t.AcademicTitleType
                }).ToListAsync(ct);
        }

        public async Task<IEnumerable<AcademicTitleRequestEntity>> GetRequestedTitlesOfUserAsync(int uid, CancellationToken ct)
        {
            return await _context.TitleRequests.Where(tr => tr.AppUserId == uid)
                .Select(rt => new AcademicTitleRequestEntity()
                {
                    Id = rt.Id,
                    CreatedAt = rt.CreatedAt,
                    Status = rt.TitleRequestStatus,
                    Title = new AcademicTitleEntity()
                    {
                        Name = rt.AcademicTitleIdNavigation.Name
                    }
                })
                .ToListAsync(ct);
        }

        public async Task<bool> SaveOrderOfAcademicTitleForUserAsync(int uid, IDictionary<int, int> orders, CancellationToken ct)
        {
            var tran = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                _context.AppUsersTitles.RemoveRange(_context.AppUsersTitles.Where(ut => ut.AppUserId == uid));
                var userTitles = orders.Select(o => new AppUserTitle()
                {
                    AppUserId = uid,
                    AcademicTitleId = o.Value,
                    Order = o.Key
                });
                await _context.AppUsersTitles.AddRangeAsync(userTitles, ct);
                await tran.CommitAsync(ct);
                return true;
            } catch
            {
                await tran.RollbackAsync(ct);
                return false;
            }
        }
    }
}
