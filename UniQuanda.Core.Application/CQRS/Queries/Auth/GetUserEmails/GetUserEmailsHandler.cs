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
        var userConfirmedEmails = await _authRepository.GetUserConfirmedEmailsAsync(request.IdUser, ct);
        var userEmailToConfirm = await _authRepository.GetUserNotConfirmedEmailAsync(request.IdUser, ct);
        if (userConfirmedEmails is null)
            return null;


        return new GetUserEmailsReponseDTO
        {
            MainEmail = userConfirmedEmails.MainEmail,
            EmailToConfirm = userEmailToConfirm,
            ExtraEmails = userConfirmedEmails.ExtraEmails
        };
    }
}