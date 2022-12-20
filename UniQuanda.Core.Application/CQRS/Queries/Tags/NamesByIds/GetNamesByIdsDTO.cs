namespace UniQuanda.Core.Application.CQRS.Queries.Tags.GetTags
{
    public class GetNamesByIdsRequestDTO
    {
        public IEnumerable<int> Ids { get; set; }
    }
    public class GetNamesByIdsResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
