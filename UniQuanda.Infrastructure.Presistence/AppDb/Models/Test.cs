namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class Test
{
    public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public bool IsFinished { get; set; }

		public int IdCreator { get; set; }
		public virtual AppUser IdCreatorNavigation { get; set; }

		public virtual ICollection<TestQuestion> TestQuestions { get; set; }
		public virtual ICollection<TestTag> TestTags { get; set; }
}