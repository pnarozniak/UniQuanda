using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Questions.UpdateQuestion;

public class UpdateQuestionCommand : IRequest<bool?>
{
    public UpdateQuestionCommand(UpdateQuestionRequestDTO request, int UserId)
    {
        this.IdQuestion = request.IdQuestion;
        this.Title = request.Title;
        this.RawText = request.RawText;
        this.UserId = UserId;
        this.Tags = request.TagIds;
        this.CreationTime = DateTime.Now;
    }

    public int IdQuestion { get; set; }
    public string Title { get; set; }
    public string RawText { get; set; }
    public int UserId { get; set; }
    public IEnumerable<int> Tags { get; set; }
    public DateTime CreationTime { get; set; }
}