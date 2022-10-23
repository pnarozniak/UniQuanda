using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Security.GetUserEmails;

public class GetUserEmailsHandler : IRequestHandler<GetUserEmailsQuery, GetUserEmailsReponseDTO>
{
    private readonly ISecurityRepository _securityRepository;

    public GetUserEmailsHandler(ISecurityRepository securityRepository)
    {
        _securityRepository = securityRepository;
    }

    public async Task<GetUserEmailsReponseDTO?> Handle(GetUserEmailsQuery request, CancellationToken ct)
    {
        var userEmails = await _securityRepository.GetUserEmailsAsync(request.IdUser, ct);
        if (userEmails is null)
            return null;

        return new GetUserEmailsReponseDTO
        {
            MainEmail = userEmails.MainEmail,
            ExtraEmails = userEmails.ExtraEmails
        };
    }
}