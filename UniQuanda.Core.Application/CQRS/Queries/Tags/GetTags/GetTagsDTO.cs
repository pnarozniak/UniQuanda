using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.Tags.GetTags
{
    public class GetTagsRequestDTO
    {
        /// <summary>
        ///     Page number
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }
        /// <summary>
        ///     Amount of tags on page
        /// </summary>
        [Required]
        [Range(1, 20)]
        public int PageSize { get; set; }
        /// <summary>
        ///     Parent tag Id
        /// </summary>
        public int? TagId { get; set; }
        /// <summary>
        ///     Text to search for
        /// </summary>
        [MaxLength(30)]
        public string? Keyword { get; set; }
        /// <summary>
        ///     If true, response will contain all records that meet the given filters
        /// </summary>
        [Required]
        public bool AddCount { get; set; }
        /// <summary>
        ///     Tag sorting direction
        /// </summary>
        [Required]
        public OrderDirectionEnum OrderDirection { get; set; }
        /// <summary>
        ///     If true and idTag is provided, the response will contain the description and name of the parent tag
        /// </summary>
        public bool? AddParentTagData { get; set; }
    }
    public class GetTagsResponseDTO
    {
        public int? TotalCount { get; set; }
        public IEnumerable<GetTagsResponseTagDTO> Tags { get; set; }
        public GetTagsResponseTagDTO? ParentTag { get; set; }
    }
    public class GetTagsResponseTagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int? ParentTagId { get; set; }
    }

}
