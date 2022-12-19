using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.App
{
    public class AnswerEntity
    {
        public int? Id { get; set; }
        public int? QuestionId { get; set; }
        public int? ParentAnswerId { get; set; }
        public bool? IsCorrect { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? Likes { get; set; }
        public Content? Content { get; set; }
        public QuestionEntity? Question { get; set; }
        public AnswerEntity? ParentAnswer { get; set; }
    }
}
