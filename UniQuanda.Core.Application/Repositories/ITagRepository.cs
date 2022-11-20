using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.Repositories
{
    public interface ITagRepository
    {
        /// <summary>
        ///     Gets parent tags using paging
        /// </summary>
        /// <param name="take">Amount of tags to take</param>
        /// <param name="skip">Amount of tags to skip</param>
        /// <param name="orderDirection">How to order data</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>IEnumerable of parent tags</returns>
        public Task<IEnumerable<TagEntity>> GetParentTagsAsync(int take, int skip, OrderDirectionEnum orderDirection, CancellationToken ct);
        
        /// <summary>
        ///     Gets sub-tags using paging
        /// </summary>
        /// <param name="take">Amount of tags to take</param>
        /// <param name="skip">Amount of tags to skip</param>
        /// <param name="tagId">Id of parent tag</param>
        /// <param name="orderDirection">How to order data</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>IEnumerable of sub-tags</returns>
        public Task<IEnumerable<TagEntity>> GetSubTagsAsync(int take, int skip, int tagId, OrderDirectionEnum orderDirection, CancellationToken ct);
        
        /// <summary>
        ///     Gets parent tags and subtags using keyword and paging
        /// </summary>
        /// <param name="take">Amount of tags to take</param>
        /// <param name="skip">Amount of tags to skip</param>
        /// <param name="keyword">Value to search in tag description and name</param>
        /// <param name="orderDirection">How to order data</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>IEnumerable of all tags with keyword</returns>
        public Task<IEnumerable<TagEntity>> GetTagsByKeywordAsync(int take, int skip, string keyword, OrderDirectionEnum orderDirection, CancellationToken ct);
        
        /// <summary>
        ///     Gets subtags using keyword and paging
        /// </summary>
        /// <param name="take">Amount of tags to take</param>
        /// <param name="skip">Amount of tags to skip</param>
        /// <param name="tagId">Id of parent tag</param>
        /// <param name="keyword">Value to search in tag description and name</param>
        /// <param name="orderDirection">How to order data</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>IEnumerable of all tags with keyword</returns>
        public Task<IEnumerable<TagEntity>> GetSubTagsByKeywordAsync(int take, int skip, string keyword, int tagId, OrderDirectionEnum orderDirection, CancellationToken ct);
        
        /// <summary>
        ///     Get parent tags count
        /// </summary>
        /// <param name="ct">Cancelation token</param>
        /// <returns>Amount of parent tags</returns>
        public Task<int> GetParentTagsCountAsync(CancellationToken ct);
        
        /// <summary>
        ///     Gets sub-tags count
        /// </summary>
        /// <param name="tagId">Id of parent tag</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>Amount of sub-tags</returns>
        public Task<int> GetSubTagsCountAsync(int tagId, CancellationToken ct);

        /// <summary>
        ///     Gets tags with matching keyword count
        /// </summary>
        /// <param name="keyword">Value to search in tag description and name</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>Amount of tags with matching keyword</returns>
        public Task<int> GetTagsByKeywordCountAsync(string keyword, CancellationToken ct);

        /// <summary>
        ///     Gets sub-tags with matching keyword count
        /// </summary>
        /// <param name="keyword">Value to search in tag description and name</param>
        /// <param name="tagId">Id of parent tag</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>Amount of tags with matching keyword</returns>
        public Task<int> GetSubTagsByKeywordCountAsync(string keyword, int tagId, CancellationToken ct);

        /// <summary>
        ///    Gets tag by id
        /// </summary>
        /// <param name="tagId">Id of tag</param>
        /// <param name="ct"></param>
        /// <returns>Cancelation token</returns>
        public Task<TagEntity> GetTagByIdAsync(int tagId, CancellationToken ct);
    }
}
