using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.Repositories
{
    public interface ITagRepository
    {
        /// <summary>
        ///     Gets main tags using paging
        /// </summary>
        /// <param name="take">Amount of tags to take</param>
        /// <param name="skip">Amount of tags to skip</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>IEnumerable of main tags</returns>
        public Task<IEnumerable<TagEntity>> GetMainTagsAsync(int take, int skip, CancellationToken ct);
        /// <summary>
        ///     Gets sub-tags using paging
        /// </summary>
        /// <param name="take">Amount of tags to take</param>
        /// <param name="skip">Amount of tags to skip</param>
        /// <param name="tagId">Id of main tag</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>IEnumerable of sub-tags</returns>
        public Task<IEnumerable<TagEntity>> GetSubTagsAsync(int take, int skip, int tagId, CancellationToken ct);
        /// <summary>
        ///     Gets tags and / or subtags using keyword and paging
        /// </summary>
        /// <param name="take">Amount of tags to take</param>
        /// <param name="skip">Amount of tags to skip</param>
        /// <param name="keyword">Value to search in tag description and name</param>
        /// <param name="parentTagId">Tag of main tag. If given search will be narrowed only to subtags</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>IEnumerable of all tags with keyword</returns>
        public Task<IEnumerable<TagEntity>> GetTagsByKeywordAsync(int take, int skip, string keyword, int? parentTagId, CancellationToken ct);
        /// <summary>
        ///     Get main tags count
        /// </summary>
        /// <param name="ct">Cancelation token</param>
        /// <returns>Amount of main tags</returns>
        public Task<int> GetMainTagsCountAsync(CancellationToken ct);
        /// <summary>
        ///     Gets sub-tags count
        /// </summary>
        /// <param name="tagId">Id of main tag</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>Amount of sub-tags</returns>
        public Task<int> GetSubTagsCountAsync(int tagId, CancellationToken ct);
        /// <summary>
        ///     Gets tags with matching keyword count
        /// </summary>
        /// <param name="keyword">Value to search in tag description and name</param>
        /// <param name="parentTagId">Tag of main tag. If given search will be narrowed only to subtags</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>Amount of tags with matching keyword</returns>
        public Task<int> GetTagsByKeywordCountAsync(string keyword, int? parentTagId, CancellationToken ct);
    }
}
