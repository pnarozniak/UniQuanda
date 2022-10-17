using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Tests.CQRS.Commands.AppUser.Settings
{
    [TestFixture]
    public class UpdateAppUserProfileHandlerTests
    {
        private UpdateAppUserProfileHandler updateAppUserProfileHandler;
        private UpdateAppUserProfileCommand updateAppUserProfileCommand;
        private Mock<IAppUserRepository> appUserRepository;

        private readonly int _idAppUser = 1;
        private readonly string _avatarUrl = "AvatarUrl";
        [SetUp]
        public void SetupTests()
        {
            this.appUserRepository = new Mock<IAppUserRepository>();
            this.updateAppUserProfileHandler = new UpdateAppUserProfileHandler(this.appUserRepository.Object);
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnIsSuccessfulAsTrue_WhenUpdateIsSuccessfulAndContainsAvatarAndBanner()
        {
            var avatar = GetImage("png");
            var banner = GetImage("png");
            var newAppUserData = GetNewAppUserData(avatar, banner);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(true, this._avatarUrl));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.IsSuccessful.Should().Be(true);
            result.AvatarUrl.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnIsSuccessfulAsTrue_WhenUpdateIsSuccessfulAndContainsAvatar()
        {
            var avatar = GetImage("png");
            var newAppUserData = GetNewAppUserData(avatar, null);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(true, this._avatarUrl));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.IsSuccessful.Should().Be(true);
            result.AvatarUrl.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnIsSuccessfulAsTrue_WhenUpdateIsSuccessfulAndContainsBanner()
        {
            var banner = GetImage("png");
            var newAppUserData = GetNewAppUserData(null, banner);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);

            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(true, null));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.IsSuccessful.Should().Be(true);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnIsSuccessfulAsTrue_WhenUpdateIsSuccessfulAndNotContainsAvatarAndBanner()
        {
            var newAppUserData = GetNewAppUserData(null, null);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(true, null));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.IsSuccessful.Should().Be(true);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnIsSuccessfulAsFalse_WhenUpdateAppUserIsNotSuccessful()
        {
            var newAppUserData = GetNewAppUserData(null, null);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(false, null));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.IsSuccessful.Should().Be(false);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnIsSuccessfulAsNull_WhenAppUserIsNotFound()
        {
            var newAppUserData = GetNewAppUserData(null, null);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(null, null));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.IsSuccessful.Should().Be(null);
            result.AvatarUrl.Should().BeNull();
        }

        private static UpdateAppUserProfileRequestDTO GetNewAppUserData(IFormFile? avatar, IFormFile? banner)
        {
            return new UpdateAppUserProfileRequestDTO
            {
                FirstName = "FirstName",
                LastName = "LastName",
                City = "City",
                AboutText = "AboutText",
                Birthdate = new DateTime(2001, 1, 1),
                PhoneNumber = "PhoneNumber",
                SemanticScholarProfile = "SemanticScholarProfile",
                Avatar = avatar,
                Banner = banner
            };
        }

        private static IFormFile GetImage(string contentType)
        {
            var file = new Mock<IFormFile>();
            file.Setup(f => f.ContentType).Returns(contentType);
            return file.Object;
        }

        private static ImageUploadResult GetImageUploadResult(bool isSuccessful, string? imageUrl)
        {
            return new ImageUploadResult
            {
                IsSuccessful = isSuccessful,
                ImageUrl = imageUrl
            };
        }

        private static AppUserUpdateResult GetAppUserProfileUpdateResult(bool? isSuccessful, string? avatarUrl)
        {
            return new AppUserUpdateResult
            {
                IsSuccessful = isSuccessful,
                AvatarUrl = avatarUrl
            };
        }
    }
}