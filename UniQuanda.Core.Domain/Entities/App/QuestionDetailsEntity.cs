using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.App;

public class QuestionDetailsEntity
{
    public int Id { get; set; }
    public string Header { get; set; }
    public string Content { get; set; }
    public bool IsQuestionFollowed { get; set; }
    public bool IsQuestionViewed { get; set; }
    public DateTime PublishDate { get; set; }
    public bool IsModified { get; set; }
    public int AmountOfAnswers { get; set; }
    public int Views { get; set; }
    public bool HasCorrectAnswer { get; set; }
    public AuthorContent Author { get; set; }
    public IEnumerable<QuestionDetailsTag> Tags { get; set; }
}