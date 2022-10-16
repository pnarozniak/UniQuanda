using MediatR;
using UniQuanda.Core.Application.Repositories;
using static UniQuanda.Core.Application.CQRS.Queries.Profile.GetProfile.GetProfileResponseDTO;

namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetProfile
{
    public class GetProfileHandler : IRequestHandler<GetProfileQuery, GetProfileResponseDTO?>
    {
        private readonly IAppUserRepository _appUserRepository;

        public GetProfileHandler(
            IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public async Task<GetProfileResponseDTO?> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _appUserRepository.GetUserProfileAsync(request.UserId, cancellationToken);
            if (user == null) return null;

            return new GetProfileResponseDTO()
            {
                UserData = new UserDataResponseDTO()
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Avatar = user.Avatar,
                    Banner = user.Banner
                },
                HeaderStatistics = new HeaderStatisticsResponseDTO()
                {
                    Answers = user.AnswersAmount ?? 0,
                    Questions = user.QuestionsAmount ?? 0,
                    Points = user.Points ?? 0
                },
                AcademicTitles = user.Titles.Select(title => new AcademicTitleResponseDTO()
                {
                    Name = title.Name,
                    AcademicTitleType = title.Type,
                    Order = title.Order
                }),
                Universities = user.Universities.Select(u => new UniversityResponseDTO()
                {
                    Id = u.Id,
                    Logo = u.Logo,
                    Name = u.Name,
                    Order = u.Order
                })
            };
        }
    }
}
