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

        Task<int?> GetIdContentOfQuestionAsync(int idQuestion, CancellationToken ct);

        Task<IEnumerable<string>> GetAllUrlImagesConnectedWithContent(int contentId, CancellationToken ct);

        Task<int?> GetIdContentOfAnswerAsync(int idAnswer, CancellationToken ct);
    }
}
