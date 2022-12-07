using MediatR;
using Microsoft.AspNetCore.Http;
using UniQuanda.Core.Application.CQRS.Commands.Questions.AddQuestion;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class AddQuestionCommand : IRequest<AddQuestionResponseDTO>
{
    public AddQuestionCommand(AddQuestionRequestDTO request, int UserId)
    {
        this.Title = request.Title;
        this.RawText = request.RawText;
        this.UserId = UserId;
        this.Tags = request.TagIds;
        this.CreationTime = DateTime.Now;
    }
    public string Title { get; set; }
    public string RawText { get; set; }
    public int UserId { get; set; }
    public IEnumerable<int> Tags { get; set; }
    public DateTime CreationTime { get; set; }
}