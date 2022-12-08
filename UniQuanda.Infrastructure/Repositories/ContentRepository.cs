using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Infrastructure.Presistence.AppDb;

namespace UniQuanda.Infrastructure.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly AppDbContext _context;
        public ContentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetNextContentIdAsync(CancellationToken ct)
        {
            return (await _context.IntFunctionWrapper
                .FromSqlRaw("SELECT nextval('\"Contents_Id_seq\"') AS result")
                .FirstAsync(ct)
                ).result;

        }
    }
}
