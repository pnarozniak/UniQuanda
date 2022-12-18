using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Answers.GetAllComments;

public class GetAllCommentsHandler : IRequestHandler<GetAllCommentsQuery, GetAllCommentsResponseDTO>
{
    private readonly IAnswerRepository _answerRepository;

    public GetAllCommentsHandler(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<GetAllCommentsResponseDTO> Handle(GetAllCommentsQuery request, CancellationToken ct)
    {
        return new GetAllCommentsResponseDTO
        {
            Comments = await _answerRepository.GetAllCommentsAsync(request.IdParentAnswer, request.IdLoggedUser, ct)
        };
    }
}