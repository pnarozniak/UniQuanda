using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.GetUserEmails;

public class GetUserEmailsHandler : IRequestHandler<GetUserEmailsQuery, GetUserEmailsReponseDTO>
{
    private readonly IAuthRepository _authRepository;

    public GetUserEmailsHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<GetUserEmailsReponseDTO?> Handle(GetUserEmailsQuery request, CancellationToken ct)
    {
        var userEmails = await _authRepository.GetUserEmailsAsync(request.IdUser, ct);
        if (userEmails is null)
            return null;

        return new GetUserEmailsReponseDTO
        {
            MainEmail = userEmails.MainEmail,
            ExtraEmails = userEmails.ExtraEmails
        };
    }
}