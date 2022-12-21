using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.App
{
    public class TestEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsFinished { get; set; }
        public int IdCreator { get; set; }
        public IEnumerable<TestTagValueObject> Tags { get; set; }
        public IEnumerable<TestQuestionValueObject>? Questions { get; set; }
    }
}
