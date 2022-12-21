using NpgsqlTypes;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class Tag
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public int? ParentTagId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public NpgsqlTsVector SearchVector { get; set; }
    public virtual Tag? ParentTagIdNavigation { get; set; }
    public virtual ICollection<Tag> ChildTags { get; set; }
    public virtual ICollection<TagInQuestion> TagInQuestions { get; set; }
    public virtual ICollection<UserPointsInTag> UsersPointsInTag { get; set; }
    public virtual ICollection<TestTag> TagInTests { get; set; }
}