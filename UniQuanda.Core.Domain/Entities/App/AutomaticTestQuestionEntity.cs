namespace UniQuanda.Core.Domain.Entities.App
{
    public class AutomaticTestQuestionEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Header { get; set; }
        public string HTML { get; set; }
        public AutomaticTestAnswerEntity Answer { get; set; }
    }
}
