using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Admin.TitleRequest.GetAllRequests
{
    public class GetAllRequestsHandler : IRequestHandler<GetAllRequestsQuery, GetAllRequestsResponseDTO>
    {
        private readonly IAcademicTitleRepository _academicTitleRepository;
        public GetAllRequestsHandler(IAcademicTitleRepository academicTitleRepository)
        {
            _academicTitleRepository = academicTitleRepository;
        }
        public async Task<GetAllRequestsResponseDTO> Handle(GetAllRequestsQuery request, CancellationToken ct)
        {
            var requests = await _academicTitleRepository.GetPendingRequestsAsync(request.Take, request.Skip, ct);
            int? totalCount = request.AddTotalCount ? await _academicTitleRepository.GetPendingRequestsCountAsync(ct) : null;
            return new GetAllRequestsResponseDTO()
            {
                Requests = requests.Select(at => new GetAllRequestsResponseDTORequest()
                {
                    Id = at.Id,
                    CreateAt = at.CreatedAt,
                    UserId = at.User.Id,
                    UserName = at.User.Nickname,
                    TitleName = at.Title.Name,
                    TitleId = at.Title.Id,
                    ScanUrl = at.Scan.URL,
                    AdditionalInfo = at.AdditionalInfo
                }),
                TotalCount = totalCount
            };

            //Id = tr.Id,
            //        CreatedAt = tr.CreatedAt,
            //        AdditionalInfo = tr.AdditionalInfo,
            //        User = new AppUserEntity()
            //        {
            //            Id = tr.AppUserId,
            //            Nickname = tr.AppIdNavigationUser.Nickname
            //        },
            //        Title = new AcademicTitleEntity()
            //        {
            //            Id = tr.AcademicTitleId,
            //            Name = tr.AcademicTitleIdNavigation.Name
            //        },
            //        Scan = new Core.Domain.ValueObjects.Image()
            //        {
            //            URL = tr.ScanIdNavigation.URL
            //        }
        }
    }
}

