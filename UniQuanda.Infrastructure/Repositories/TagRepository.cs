using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Infrastructure.Presistence.AppDb;

namespace UniQuanda.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;
        public TagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagEntity>> GetMainTagsAsync(int take, int skip, CancellationToken ct)
        {
            return await _context.Tags
                .Where(t => t.ParentTagId == null)
                .Select(t => new TagEntity()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }

        public async Task<int> GetMainTagsCountAsync(CancellationToken ct)
        {
            return await _context.Tags
                .Where(t => t.ParentTagId == null)
                .CountAsync(ct);
        }

        public async Task<IEnumerable<TagEntity>> GetSubTagsAsync(int take, int skip, int tagId, CancellationToken ct)
        {
            return await _context.Tags
                .Where(t => t.ParentTagId == tagId)
                .Select(t => new TagEntity()
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImageUrl = t.ImageUrl
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }

        public async Task<int> GetSubTagsCountAsync(int tagId, CancellationToken ct)
        {
            return await _context.Tags
                .Where(t => t.ParentTagId == tagId)
                .CountAsync(ct);
        }

        public async Task<IEnumerable<TagEntity>> GetTagsByKeywordAsync(int take, int skip, string keyword, int? parentTagId, CancellationToken ct)
        {
            var query = _context.Tags
                .Where(t =>
                    t.SearchVector.Matches(EF.Functions.PlainToTsQuery("polish", keyword))
                    || EF.Functions.ILike(t.Name, $"%{keyword}%")
                );
            if (parentTagId != null)
            {
                return await query.Where(t => t.ParentTagId == parentTagId)
                    .Select(t => new TagEntity()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        ImageUrl = t.ImageUrl
                    })
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync(ct);
            }
            return await query
                .Select(t => new TagEntity()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);

        }

        public async Task<int> GetTagsByKeywordCountAsync(string keyword, int? parentTagId, CancellationToken ct)
        {
            var query = _context.Tags
                .Where(t =>
                    t.SearchVector.Matches(EF.Functions.PlainToTsQuery("polish", keyword))
                    || EF.Functions.ILike(t.Name, $"%{keyword}%")
                );
            if (parentTagId != null)
            {
                query = query.Where(t => t.ParentTagId == parentTagId);
            }
            if (parentTagId != null)
            {
                query = query.Where(t => t.ParentTagId == parentTagId);
            }
            return await query.CountAsync(ct);
        }
    }
}
