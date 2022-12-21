namespace UniQuanda.Core.Domain.ValueObjects
{
    public class TestQuestionValueObject
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Header { get; set; }
        public string HTML { get; set; }
        public TestAnswerValueObject Answer { get; set; }
    }
}
