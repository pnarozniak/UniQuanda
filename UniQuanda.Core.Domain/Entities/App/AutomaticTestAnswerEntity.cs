namespace UniQuanda.Core.Domain.Entities.App
{
    public class AutomaticTestAnswerEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string HTML { get; set; }
        public int CommentsCount { get; set; }
    }
}
