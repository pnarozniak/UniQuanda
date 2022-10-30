using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.Tags.GetTags
{
    public class GetTagsRequestDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }
        [Required]
        [Range(1, 20)]
        public int PageSize { get; set; }
        public int? TagId { get; set; }
        [MaxLength(30)]
        public string? Keyword { get; set; }
        [Required]
        public bool AddCount { get; set; }
        [Required]
        public OrderDirectionEnum OrderDirection { get; set; }
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
