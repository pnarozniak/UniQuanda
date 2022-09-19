namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class Tag
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public int? ParentTagId { get; set; }
    public virtual Tag? ParentTagIdNavigation { get; set; }
    public virtual ICollection<Tag> ChildTags { get; set; } 
    public virtual ICollection<TagInQuestionstion> TagInQuestions { get; set; }
    public virtual ICollection<UserPointsInTag> UsersPointsInTag { get; set; }
}