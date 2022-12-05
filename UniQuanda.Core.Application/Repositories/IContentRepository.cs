namespace UniQuanda.Core.Application.Repositories
{
    public interface IContentRepository
    {
        public Task<int> GetNextContentIdAsync();
    }
}
