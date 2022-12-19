using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Admin.Titles.AssignStatusToRequest
{
    public class AssignStatusToRequestHandler : IRequestHandler<AssignStatusToRequestCommand, bool>
    {
        private readonly IAcademicTitleRepository _academicTitleRepository;
        public AssignStatusToRequestHandler(IAcademicTitleRepository academicTitleRepository)
        {
            _academicTitleRepository = academicTitleRepository;
        }

        public async Task<bool> Handle(AssignStatusToRequestCommand request, CancellationToken ct)
        {
            var requestedTitle = await _academicTitleRepository.GetRequestByIdAsync(request.RequestId, ct);
            if (requestedTitle == null) return false;

            var result = await _academicTitleRepository.AssingStatusToRequestForAcademicTitleAsync(request.RequestId, request.Status, ct);

            if (result && request.Status == Domain.Enums.TitleRequestStatusEnum.Accepted)
            {
                return await _academicTitleRepository.SetAcademicTitleToUserAsync(requestedTitle.User.Id, requestedTitle.Title.Id, null, ct);
            }
            return result;
        }
    }
}

