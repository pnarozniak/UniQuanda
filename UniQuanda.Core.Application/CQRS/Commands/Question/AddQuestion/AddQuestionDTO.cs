namespace UniQuanda.Core.Application.CQRS.Commands.Question.AddQuestion;

public class AddQuestionRequestDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}