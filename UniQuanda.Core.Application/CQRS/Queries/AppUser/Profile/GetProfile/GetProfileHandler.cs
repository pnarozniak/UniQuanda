﻿using MediatR;
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

            var userHasPremium = await _appUserRepository.HasUserPremiumAsync(request.UserId, cancellationToken);
            return new GetProfileResponseDTO()
            {
                UserData = new UserDataResponseDTO()
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Avatar = user.Avatar,
                    Banner = user.Banner,
                    AboutText = user.AboutText,
                    Contact = user.Contact,
                    City = user.City,
                    Birthdate = user.Birthdate,
                    SemanticScholarProfile = user.SemanticScholarProfile,
                    HasPremium = userHasPremium
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
                    AcademicTitleType = title.AcademicTitleType,
                    Order = title.Order
                }),
                Universities = user.Universities.Select(u => new UniversityResponseDTO()
                {
                    Id = u.Id,
                    Logo = u.Icon,
                    Name = u.Name,
                    Order = u.Order
                }),
                PointsInTags = user.Tags.Select(tag => new PointsInTagsResponseDTO()
                {
                    Points = tag.Points,
                    Name = tag.Name
                })
            };
        }
    }
}
