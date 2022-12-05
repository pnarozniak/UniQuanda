namespace UniQuanda.Core.Application.Repositories
{
    public interface IQuestionRepository
    {
        /// <summary>
        ///    Add question to database
        /// </summary>
        /// <param name="contentId">id of content</param>
        /// <param name="userId">id of creator</param>
        /// <param name="tags">tag ids with order</param>
        /// <param name="title">question title</param>
        /// <param name="rawText">not modified, html text</param>
        /// <param name="text">only text from html</param>
        /// <param name="imageIds">all images of content</param>
        /// <param name="ct">cancellation token</param>
        /// <returns></returns>
        public Task<int> AddQuestionAsync(
            int contentId, int userId, 
            IEnumerable<(int order, int tagId)> tags, 
            string title, string rawText, 
            string text, IEnumerable<int> imageIds,
            CancellationToken ct
            );
    }
}
