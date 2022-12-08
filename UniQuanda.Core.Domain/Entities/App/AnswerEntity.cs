namespace UniQuanda.Core.Domain.Entities.App
{
    public class AnswerEntity
    {
        public int? Id { get; set; }
        public QuestionEntity? Question { get; set; }
        public AnswerEntity? ParentAnswer { get; set; }
    }
}
