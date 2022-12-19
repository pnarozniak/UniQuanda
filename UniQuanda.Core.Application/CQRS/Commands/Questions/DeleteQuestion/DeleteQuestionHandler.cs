using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Questions.DeleteQuestion;

public class DeleteQuestionHandler : IRequestHandler<DeleteQuestionCommand, DeleteQuestionResponseDTO>
{
    private readonly IQuestionRepository _questionRepository;
    public DeleteQuestionHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    public async Task<DeleteQuestionResponseDTO> Handle(DeleteQuestionCommand request, CancellationToken ct)
    {
        return new DeleteQuestionResponseDTO
        {
            Status = await _questionRepository.DeleteQuestionAsync(request.IdQuestion, request.IdLoggedUser, ct)
        };
    }
}