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

        public Task<int> GetAnswersOfUserCountAsync(int userId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
