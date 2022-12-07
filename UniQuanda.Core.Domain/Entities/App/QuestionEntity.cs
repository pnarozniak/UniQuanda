using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.App
{
    public class QuestionEntity
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ViewsCount { get; set; }
        public Content Content { get; set; }
    }
}
