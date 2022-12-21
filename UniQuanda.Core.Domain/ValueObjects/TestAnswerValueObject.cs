namespace UniQuanda.Core.Domain.ValueObjects
{
    public class TestAnswerValueObject
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string HTML { get; set; }
        public int CommentsCount { get; set; }
    }
}
