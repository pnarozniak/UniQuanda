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

        public async Task<int?> GetIdContentOfQuestionAsync(int idQuestion, CancellationToken ct)
        {
            var question = await _context.Questions.Include(q => q.ContentIdNavigation).SingleOrDefaultAsync(q => q.Id == idQuestion, ct);
            return question?.ContentIdNavigation.Id;
        }

        public async Task<IEnumerable<string>> GetAllUrlImagesConnectedWithContent(int contentId, CancellationToken ct)
        {
            return await _context.ImagesInContent.Where(c => c.ContentId == contentId).Select(c => c.ImageIdNavigation.URL).ToListAsync(ct);
        }

        public async Task<int?> GetIdContentOfAnswerAsync(int idAnswer, CancellationToken ct)
        {
            var answer = await _context.Answers.Include(q => q.ContentIdNavigation).SingleOrDefaultAsync(q => q.Id == idAnswer, ct);
            return answer?.ContentIdNavigation.Id;
        }
    }
}
