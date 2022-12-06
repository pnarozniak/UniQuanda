using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.ExtensionsEF;

namespace UniQuanda.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;
        public TagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagEntity>> GetParentTagsAsync(
            int take, int skip,
            OrderDirectionEnum orderDirection,
            CancellationToken ct)
        {
            return await _context.Tags
                .Where(t => t.ParentTagId == null)
                .OrderBy(t => t.Name, orderDirection)
                .Skip(skip)
                .Take(take)
                .Select(t => new TagEntity()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    ImageUrl = t.ImageUrl,
                    ParentId = t.ParentTagId
                })
                .ToListAsync(ct);
        }

        public async Task<int> GetParentTagsCountAsync(CancellationToken ct)
        {
            return await _context.Tags
                .Where(t => t.ParentTagId == null)
                .CountAsync(ct);
        }

        public async Task<IEnumerable<TagEntity>> GetSubTagsAsync(
            int take, int skip, int tagId,
            OrderDirectionEnum orderDirection,
            CancellationToken ct)
        {
            return await _context.Tags
                .Where(t => t.ParentTagId == tagId)
                .OrderBy(t => t.Name, orderDirection).Skip(skip)
                .Take(take)
                .Select(t => new TagEntity()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    ImageUrl = t.ImageUrl,
                    ParentId = t.ParentTagId
                })
                .ToListAsync(ct);
        }

        public async Task<int> GetSubTagsCountAsync(int tagId, CancellationToken ct)
        {
            return await _context.Tags
                .Where(t => t.ParentTagId == tagId)
                .CountAsync(ct);
        }

        public async Task<TagEntity?> GetTagByIdAsync(int tagId, CancellationToken ct)
        {
            return await _context.Tags
                .Where(t => t.Id == tagId)
                .Select(t => new TagEntity()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    ImageUrl = t.ImageUrl,
                    ParentId = t.ParentTagId
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IEnumerable<TagEntity>> GetTagsByKeywordAsync(
            int take, int skip,
            string keyword,
            OrderDirectionEnum orderDirection,
            CancellationToken ct)
        {
            return await _context.Tags
                .Where(t =>
                    t.SearchVector.Matches(EF.Functions.PlainToTsQuery("polish", keyword))
                    || EF.Functions.ILike(t.Name, $"%{keyword}%")
                ).OrderBy(t => t.Name, orderDirection)
                .Select(t => new TagEntity()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    ImageUrl = t.ImageUrl,
                    ParentId = t.ParentTagId
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);

        }

        public async Task<int> GetTagsByKeywordCountAsync(string keyword, CancellationToken ct)
        {
            return await _context.Tags
               .Where(t =>
                   t.SearchVector.Matches(EF.Functions.PlainToTsQuery("polish", keyword))
                   || EF.Functions.ILike(t.Name, $"%{keyword}%")
               ).CountAsync(ct);
        }

        public async Task<IEnumerable<TagEntity>> GetSubTagsByKeywordAsync(
            int take, int skip,
            string keyword, int tagId,
            OrderDirectionEnum orderDirection,
            CancellationToken ct)
        {
            return await _context.Tags
                    .Where(t =>
                        (t.SearchVector.Matches(EF.Functions.PlainToTsQuery("polish", keyword))
                        || EF.Functions.ILike(t.Name, $"%{keyword}%"))
                        && t.ParentTagId == tagId
                    ).OrderBy(t => t.Name, orderDirection)
                    .Select(t => new TagEntity()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        ImageUrl = t.ImageUrl,
                        ParentId = t.ParentTagId
                    })
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync(ct);
        }

        public async Task<int> GetSubTagsByKeywordCountAsync(string keyword, int tagId, CancellationToken ct)
        {
            return await _context.Tags
               .Where(t =>
                   (t.SearchVector.Matches(EF.Functions.PlainToTsQuery("polish", keyword))
                   || EF.Functions.ILike(t.Name, $"%{keyword}%"))
                   && t.ParentTagId == tagId
               ).CountAsync(ct);
        }

        public async Task<bool> CheckIfAllTagIdsExistAsync(IEnumerable<int> tagIds, CancellationToken ct)
        {
            var count = await _context.Tags.Where(t => tagIds.Contains(t.Id))
                .CountAsync(ct);
            return count == tagIds.Count();
        }
    }
}
