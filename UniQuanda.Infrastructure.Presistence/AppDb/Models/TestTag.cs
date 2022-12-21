namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class TestTag
{
    public int Id { get; set; }

		public int IdTest { get; set; }
		public Test IdTestNavigation { get; set; }

		public int IdTag { get; set; }
		public Tag IdTagNavigation { get; set; }
}