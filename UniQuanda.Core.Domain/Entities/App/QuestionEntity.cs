using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.App
{
    public class QuestionEntity
    {
        public int? Id { get; set; }
        public string? Header { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ViewsCount { get; set; }
        public Content? Content { get; set; }
        public bool? IsPopular { get; set; }
        public bool? HasCorrectAnswer { get; set; }
        public AppUserEntity? User { get; set; }
        public IEnumerable<TagEntity>? Tags { get; set; }
        public IEnumerable<AnswerEntity>? Answers { get; set; }
        public int? AnswersCount { get; set; }
    }
}
