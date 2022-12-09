using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Infrastructure.Presistence.AppDb;

namespace UniQuanda.Infrastructure.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _context;
        public AnswerRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<AnswerEntity>> GetAnswersOfUserAsync(int userId, int take, int skip, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetAnswersOfUserCountAsync(int userId, CancellationToken ct)
        {
            return await _context.AppUsersAnswersInteractions.Where(aai => aai.IsCreator && aai.AppUserId == userId).CountAsync(ct);
        }
    }
}
