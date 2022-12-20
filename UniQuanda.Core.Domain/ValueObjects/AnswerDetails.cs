namespace UniQuanda.Core.Domain.ValueObjects;

public class AnswerDetails
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public bool IsModified { get; set; }
    public DateTime PublishDate { get; set; }
    public bool IsCorrect { get; set; }
    public string Content { get; set; }
    public int Likes { get; set; }
    public int UserLikeValue { get; set; }
    public int CommentsAmount { get; set; }
    public AuthorContent Author { get; set; }
    public IEnumerable<AnswerDetails> Comments { get; set; }
}