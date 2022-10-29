using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Tags.GetTags
{
    public class GetTagsRequestDTO
    {
        [Required]
        public int Page { get; set; }
        [Required]
        [Range(1, 20)]
        public int PageSize { get; set; }
        public int? TagId { get; set; }
        [MaxLength(30)]
        public string? Keyword { get; set; }
    }
    public class GetTagsResponseDTO
    {
        public int? TotalCount { get; set; }
        public IEnumerable<GetTagsResponseTagDTO> Tags { get; set; }
    }
    public class GetTagsResponseTagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }

}
