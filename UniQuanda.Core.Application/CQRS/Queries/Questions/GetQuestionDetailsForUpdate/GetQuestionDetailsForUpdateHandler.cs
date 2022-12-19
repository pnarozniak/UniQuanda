using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestionDetailsForUpdate;

public class GetQuestionDetailsForUpdateHandler : IRequestHandler<GetQuestionDetailsForUpdateQuery, GetQuestionDetailsForUpdateResponseDTO?>
{
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionDetailsForUpdateHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<GetQuestionDetailsForUpdateResponseDTO?> Handle(GetQuestionDetailsForUpdateQuery request, CancellationToken ct)
    {
        var result = await _questionRepository.GetQuestionDetailsForUpdateAsync(request.IdQuestion, request.IdLoggedUser, ct);
        if (result is null)
            return null;
        return new GetQuestionDetailsForUpdateResponseDTO
        {
            Title = result.Header,
            RawText = result.Content,
            Tags = result.Tags
        };
    }
}
