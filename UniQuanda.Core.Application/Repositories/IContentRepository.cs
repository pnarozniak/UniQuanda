namespace UniQuanda.Core.Application.Repositories
{
    public interface IContentRepository
    {
        /// <summary>
        ///     Gets next sequence number for content
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Id of next content</returns>
        public Task<int> GetNextContentIdAsync(CancellationToken ct);
    }
}
