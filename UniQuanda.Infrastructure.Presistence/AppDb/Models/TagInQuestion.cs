namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class TagInQuestion
{
    public int Id { get; set; }
    public int TagId { get; set; }
    public int QuestionId { get; set; }
    public int Order { get; set; }
    public virtual Question QuestionIdNavigation { get; set; }
    public virtual Tag TagIdNavigation { get; set; }
}