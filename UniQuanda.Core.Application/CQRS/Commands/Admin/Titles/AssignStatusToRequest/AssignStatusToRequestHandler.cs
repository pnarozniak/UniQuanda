using System;
using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Admin.Titles.AssignStatusToRequest
{
	public class AssignStatusToRequestHandler : IRequestHandler<AssignStatusToRequestCommand, bool>
    {
        private readonly IAcademicTitleRepository _academicTitleRepository;
        private readonly IRoleRepository _roleRepository;
        public AssignStatusToRequestHandler(IAcademicTitleRepository academicTitleRepository, IRoleRepository roleRepository)
		{
            _academicTitleRepository = academicTitleRepository;
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(AssignStatusToRequestCommand request, CancellationToken ct)
        {
            var requestedTitle = await _academicTitleRepository.GetRequestByIdAsync(request.RequestId, ct);
            if (requestedTitle == null) return false;

            var result = await _academicTitleRepository.AssingStatusToRequestForAcademicTitleAsync(request.RequestId, request.Status, ct);

            if(result && request.Status == Domain.Enums.TitleRequestStatusEnum.Accepted)
            {
                await _academicTitleRepository.SetAcademicTitleToUserAsync(requestedTitle.User.Id, requestedTitle.Title.Id, null, ct);
                return await _roleRepository.AssignAppRoleToUserAsync(requestedTitle.User.Id, new AppRole() { Value = AppRole.TitledUser }, null, ct);
            }
            return result;
        }
    }
}

