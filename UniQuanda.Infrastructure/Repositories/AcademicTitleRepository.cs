using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
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

        public async Task<bool> AssingStatusToRequestForAcademicTitleAsync(int requestId, TitleRequestStatusEnum status, CancellationToken ct)
        {
            var request = await _context.TitleRequests.Where(tr => tr.Id == requestId).SingleOrDefaultAsync(ct);
            if (request == null) return false;
            request.TitleRequestStatus = status;
            return await _context.SaveChangesAsync(ct) > 0;
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

        public async Task<IEnumerable<AcademicTitleRequestEntity>> GetPendingRequestsAsync(int take, int skip, CancellationToken ct)
        {
            return await _context.TitleRequests
                .Where(tr => tr.TitleRequestStatus == TitleRequestStatusEnum.Pending)
                .OrderBy(tr => tr.CreatedAt)
                .Select(tr => new AcademicTitleRequestEntity()
                {
                    Id = tr.Id,
                    CreatedAt = tr.CreatedAt,
                    AdditionalInfo = tr.AdditionalInfo,
                    User = new AppUserEntity()
                        {
                            Id = tr.AppUserId,
                            Nickname = tr.AppIdNavigationUser.Nickname
                        },
                    Title = new AcademicTitleEntity()
                    {
                        Id = tr.AcademicTitleId,
                        Name = tr.AcademicTitleIdNavigation.Name
                    },
                    Scan = new Core.Domain.ValueObjects.Image()
                    {
                        URL = tr.ScanIdNavigation.URL
                    }
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);

        }

        public async Task<int> GetPendingRequestsCountAsync(CancellationToken ct)
        {
            return await _context.TitleRequests
                .Where(tr => tr.TitleRequestStatus == TitleRequestStatusEnum.Pending)
                .CountAsync(ct);
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

        public async Task<AcademicTitleRequestEntity?> GetRequestByIdAsync(int requestId, CancellationToken ct)
        {
            return await _context.TitleRequests.Where(tr => tr.Id == requestId)
                .Select(tr => new AcademicTitleRequestEntity()
                {
                    Id = tr.Id,
                    Title = new AcademicTitleEntity()
                    {
                        Id = tr.AcademicTitleId        
                    },
                    User = new AppUserEntity()
                    {
                        Id = tr.Id
                    }
                    
                }).SingleOrDefaultAsync(ct);
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

        public async Task<bool> SetAcademicTitleToUserAsync(int userId, int titleId, int? order, CancellationToken ct)
        {
            var titleToRemove = await _context.AppUsersTitles
                .Where(ut => ut.AppUserId == userId &&
                    ut.AcademicTitleIdNavigation.AcademicTitleType == _context
                        .AcademicTitles
                        .Where(t => t.Id == titleId)
                        .Single().AcademicTitleType
                    )
                .FirstOrDefaultAsync(ct);

            if (titleToRemove != null)
                _context.AppUsersTitles.Remove(titleToRemove);

            var intOrder = order;
            if(intOrder == null)
            {
                intOrder = await _context.AppUsersTitles.Where(t => t.AppUserId == userId).MaxAsync(t => t.Order, ct);
            }

            await _context.AppUsersTitles.AddAsync(new AppUserTitle()
            {
                AcademicTitleId = titleId,
                AppUserId = userId,
                Order = intOrder ?? 0
            });
            return await _context.SaveChangesAsync(ct) > 0;
        }
    }
}
