using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Queries.Profile.GetProfile;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Tests.CQRS.Queries.Profile.GetProfile
{
    [TestFixture]
    public class GetProfileHandlerTests
    {
        private Mock<IAppUserRepository> appUserRepository;
        private GetProfileQuery getProfileQuery;

        private int UserId = 1;
        private string FirstName = "FirstName";
        private string LastName = "LastName";
        private string Nickname = "Nickname";
        private int AnswersAmount = 0;
        private int QuestionsAmount = 0;
        private int Points = 0;
        private string Avatar = "Avatar";
        private string Banner = "Banner";
        private string Tiltle = "Title";
        private string UniversityName = "UniversityName";
        private string Logo = "Logo";


        [SetUp]
        public void SetupTests()
        {
            appUserRepository = new Mock<IAppUserRepository>();
            getProfileQuery = new GetProfileQuery(new GetProfileRequestDTO
            {
                UserId = UserId
            });
        }
        [Test]
        public async Task GetProfile_ShouldReturnNull_WhenProfileDoesntExist()
        {
            appUserRepository.Setup(aur => aur.GetUserProfile(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as AppUserEntity);

            var getProfileHandler = new GetProfileHandler(appUserRepository.Object);
            var result = await getProfileHandler.Handle(getProfileQuery, new CancellationToken());
            result.Should().Be(null);
        }

        [Test]
        public async Task GetProfile_ShouldReturnProfileWithoutTitlesAndUniversities_WhenProfileExists()
        {
            var appUserEntity = GetDefaultAppUserEntity();
            appUserRepository.Setup(aur => aur.GetUserProfile(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(appUserEntity);

            var getProfileHandler = new GetProfileHandler(appUserRepository.Object);
            var result = await getProfileHandler.Handle(getProfileQuery, new CancellationToken());

            result.UserData.Id.Should().Be(UserId);
            result.Universities.Should().BeEmpty();
            result.AcademicTitles.Should().BeEmpty();
            result.UserData.FirstName.Should().Be(FirstName);
            result.UserData.LastName.Should().Be(LastName);
            result.UserData.Nickname.Should().Be(Nickname);
            result.HeaderStatistics.Answers.Should().Be(AnswersAmount);
            result.HeaderStatistics.Questions.Should().Be(QuestionsAmount);
            result.HeaderStatistics.Points.Should().Be(Points);
            result.UserData.Avatar.Should().Be(Avatar);
            result.UserData.Banner.Should().Be(Banner);

        }

        [Test]
        public async Task GetProfile_ShouldReturnProfileWithTwoAcademicTitlesWithUniqueOrderAndUniqueType_WhenProfileExists()
        {
            var appUserEntity = GetDefaultAppUserEntity();
            appUserEntity.Titles = GetAcademicTitles(2);
            appUserRepository.Setup(aur => aur.GetUserProfile(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(appUserEntity);

            var getProfileHandler = new GetProfileHandler(appUserRepository.Object);
            var result = await getProfileHandler.Handle(getProfileQuery, new CancellationToken());

            var orders = result.AcademicTitles.Select(t => t.Order).ToArray();
            var types = result.AcademicTitles.Select(t => t.AcademicTitleType).ToArray();


            result.AcademicTitles.Should().HaveCount(2);
            Assert.That(orders, Is.Unique);
            Assert.That(types, Is.Unique);

            result.UserData.Id.Should().Be(UserId);
            result.Universities.Should().BeEmpty();
            result.UserData.FirstName.Should().Be(FirstName);
            result.UserData.LastName.Should().Be(LastName);
            result.UserData.Nickname.Should().Be(Nickname);
            result.HeaderStatistics.Answers.Should().Be(AnswersAmount);
            result.HeaderStatistics.Questions.Should().Be(QuestionsAmount);
            result.HeaderStatistics.Points.Should().Be(Points);
            result.UserData.Avatar.Should().Be(Avatar);
            result.UserData.Banner.Should().Be(Banner);

        }

        [Test]
        public async Task GetProfile_ShouldReturnProfileWithTwoUniversitiesWithUniqueOrder_WhenProfileExists()
        {
            var appUserEntity = GetDefaultAppUserEntity();
            appUserEntity.Universities = GetUniversities(2);

            appUserRepository.Setup(aur => aur.GetUserProfile(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(appUserEntity);

            var getProfileHandler = new GetProfileHandler(appUserRepository.Object);
            var result = await getProfileHandler.Handle(getProfileQuery, new CancellationToken());

            var orders = result.Universities.Select(t => t.Order).ToArray();

            result.Universities.Should().HaveCount(2);
            Assert.That(orders, Is.Unique);

            result.UserData.Id.Should().Be(UserId);
            result.AcademicTitles.Should().BeEmpty();
            result.UserData.FirstName.Should().Be(FirstName);
            result.UserData.LastName.Should().Be(LastName);
            result.UserData.Nickname.Should().Be(Nickname);
            result.HeaderStatistics.Answers.Should().Be(AnswersAmount);
            result.HeaderStatistics.Questions.Should().Be(QuestionsAmount);
            result.HeaderStatistics.Points.Should().Be(Points);
            result.UserData.Avatar.Should().Be(Avatar);
            result.UserData.Banner.Should().Be(Banner);

        }

        public AppUserEntity GetDefaultAppUserEntity()
        {
            return new AppUserEntity
            {
                Id = UserId,
                FirstName = FirstName,
                LastName = LastName,
                Nickname = Nickname,
                AnswersAmount = AnswersAmount,
                QuestionsAmount = QuestionsAmount,
                Points = Points,
                Avatar = Avatar,
                Banner = Banner,
                Titles = new List<AcademicTitleEntity>(),
                Universities = new List<UniversityEntity>()
            };
        }

        public IEnumerable<AcademicTitleEntity> GetAcademicTitles(int amount)
        {
            var academicTitles = new List<AcademicTitleEntity>();
            for (var i = 0; i < amount; i++)
            {
                var type = AcademicTitleEnum.ENGINEER;
                if (i % 3 == 1)
                    type = AcademicTitleEnum.BACHELOR;
                else if (i % 3 == 2)
                    type = AcademicTitleEnum.ACADEMIC;
                academicTitles.Add(new AcademicTitleEntity
                {
                    Id = i,
                    Name = $"{Tiltle}{i}",
                    Type = type,
                    Order = i
                });
            }

            return academicTitles;
        }

        public IEnumerable<UniversityEntity> GetUniversities(int amount)
        {
            var universities = new List<UniversityEntity>();
            for (var i = 0; i < amount; i++)
            {
                universities.Add(new UniversityEntity
                {
                    Id = i,
                    Name = $"{UniversityName}{i}",
                    Order = i,
                    Logo = $"{Logo}{i}"
                });
            }
            return universities;
        }
    }
}
