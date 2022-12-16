namespace UniQuanda.Core.Domain.Entities.App
{
    public class UniversityEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Icon { get; set; }
        public string? Contact { get; set; }
        public string Regex { get; set; }
        public int Order { get; set; }
    }
}
