namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class TestQuestion
{
    public int Id { get; set; }

		public int IdTest { get; set; }
		public Test IdTestNavigation { get; set; }

		public int IdQuestion { get; set; }
		public Question IdQuestionNavigation { get; set; }
}