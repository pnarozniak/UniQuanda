using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Questions.UpdateQuestionView;

public class UpdateQuestionHandler : IRequestHandler<UpdateQuestionViewCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public UpdateQuestionHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Unit> Handle(UpdateQuestionViewCommand request, CancellationToken ct)
    {
        if (request.IdLoggedUser != null)
        {
            var isSuccess = await _questionRepository.CreateOrUpdateQuestionViewFromAppUserAsync(request.IdQuestion, request.IdLoggedUser.Value, ct);
            if (isSuccess)
                await _questionRepository.UpdateQuestionViewsCountAsync(request.IdQuestion, ct);
        }
        else
        {
            await _questionRepository.UpdateQuestionViewsCountAsync(request.IdQuestion, ct);
        }

        return Unit.Value;
    }
}