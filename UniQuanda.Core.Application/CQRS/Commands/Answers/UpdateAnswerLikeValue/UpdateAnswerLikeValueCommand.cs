using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.UpdateAnswerLikeValue;

public class UpdateAnswerLikeValueCommand : IRequest<UpdateAnswerLikeValueResponseDTO>
{
    public UpdateAnswerLikeValueCommand(UpdateAnswerLikeValueRequestDTO request, int idLoggedUser)
    {
        IdAnswer = request.IdAnswer!.Value;
        LikeValue = request.LikeValue!.Value;
        IdLoggedUser = idLoggedUser;
    }

    public int IdAnswer { get; set; }
    public int LikeValue { get; set; }
    public int IdLoggedUser { get; set; }
}