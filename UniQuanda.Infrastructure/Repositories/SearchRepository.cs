using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Infrastructure.Presistence.AppDb;

public class SearchRepository : ISearchRepository
{
    private readonly AppDbContext _appContext;
    public SearchRepository(AppDbContext appContext)
    {
        _appContext = appContext;
    }

		public async Task<IEnumerable<QuestionEntity>> SearchQuestionsAsync(string searchText, CancellationToken ct)
		{
				return await _appContext.Questions
					.Where(q => EF.Functions.ILike(q.Header, $"%{searchText}%"))
					.Take(5)
					.Select(q => new QuestionEntity() { Id = q.Id, Header = q.Header })
					.ToListAsync();
		}

		public async Task<IEnumerable<UniversityEntity>> SearchUniversitiesAsync(string searchText, CancellationToken ct)
		{
				return await _appContext.Universities
					.Where(u => EF.Functions.ILike(u.Name, $"%{searchText}%"))
					.Take(2)
					.Select(u => new UniversityEntity() { Id = u.Id, Name = u.Name })
					.ToListAsync();
		}

		public async Task<IEnumerable<AppUserEntity>> SearchUsersAsync(string searchText, CancellationToken ct)
		{
				return await _appContext.AppUsers
					.Where(u => EF.Functions.ILike(u.Nickname, $"{searchText}%"))
					.Take(3)
					.Select(u => new AppUserEntity() { Id = u.Id, Nickname = u.Nickname })
					.ToListAsync();
		}
}