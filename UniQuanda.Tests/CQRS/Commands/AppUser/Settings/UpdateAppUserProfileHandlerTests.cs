using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Tests.CQRS.Commands.AppUser.Settings
{
    [TestFixture]
    public class UpdateAppUserProfileHandlerTests
    {
        private UpdateAppUserProfileHandler updateAppUserProfileHandler;
        private UpdateAppUserProfileCommand updateAppUserProfileCommand;
        private Mock<IAppUserRepository> appUserRepository;
        private Mock<IImageService> imageService;

        private const int IdAppUser = 1;
        private const string ImageUrl = "https://ImageUrl";
        private static readonly string _avatarName = $"avatar-{IdAppUser}";
        private static readonly string _bannerName = $"banner-{IdAppUser}";

        [SetUp]
        public void SetupTests()
        {
            this.appUserRepository = new Mock<IAppUserRepository>();
            this.imageService = new Mock<IImageService>();

            this.imageService
                .Setup(i => i.GetImageURL())
                .Returns(ImageUrl);

            this.updateAppUserProfileHandler = new UpdateAppUserProfileHandler(this.appUserRepository.Object, this.imageService.Object);
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsSuccessful_WhenUpdateIsSuccessfulAndContainsAvatarAndBanner()
        {
            var isNewAvatar = true;
            var isNewBanner = true;
            this.SetUpdateAppUserProfileCommand(isNewAvatar, isNewBanner);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(IdAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.SetSaveImage(_avatarName, true);
            this.SetSaveImage(_bannerName, true);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, isNewAvatar, isNewBanner, CancellationToken.None))
                .ReturnsAsync(true);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.UpdateStatus.Should().Be(AppUserProfileUpdateStatusEnum.Successful);
            result.AvatarUrl.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsSuccessful_WhenUpdateIsSuccessfulAndContainsAvatar()
        {
            var isNewAvatar = true;
            var isNewBanner = false;
            this.SetUpdateAppUserProfileCommand(isNewAvatar, isNewBanner);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(IdAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.SetSaveImage(_avatarName, true);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, isNewAvatar, isNewBanner, CancellationToken.None))
                .ReturnsAsync(true);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.UpdateStatus.Should().Be(AppUserProfileUpdateStatusEnum.Successful);
            result.AvatarUrl.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsSuccessful_WhenUpdateIsSuccessfulAndContainsBanner()
        {
            var isNewAvatar = false;
            var isNewBanner = true;
            this.SetUpdateAppUserProfileCommand(isNewAvatar, isNewBanner);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(IdAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.SetSaveImage(_bannerName, true);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, isNewAvatar, isNewBanner, CancellationToken.None))
                .ReturnsAsync(true);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.UpdateStatus.Should().Be(AppUserProfileUpdateStatusEnum.Successful);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsSuccessful_WhenUpdateIsSuccessfulAndNotContainsAvatarAndBanner()
        {
            var isNewAvatar = false;
            var isNewBanner = false;
            this.SetUpdateAppUserProfileCommand(isNewAvatar, isNewBanner);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(IdAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, isNewAvatar, isNewBanner, CancellationToken.None))
                .ReturnsAsync(true);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.UpdateStatus.Should().Be(AppUserProfileUpdateStatusEnum.Successful);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsUnSuccessful_WhenUpdateAppUserIsNotSuccessful()
        {
            var isNewAvatar = false;
            var isNewBanner = false;
            this.SetUpdateAppUserProfileCommand(isNewAvatar, isNewBanner);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(IdAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, isNewAvatar, isNewBanner, CancellationToken.None))
                .ReturnsAsync(false);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.UpdateStatus.Should().Be(AppUserProfileUpdateStatusEnum.UnSuccessful);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsAppUserNotExist_WhenAppUserIsNotFound()
        {
            var isNewAvatar = false;
            var isNewBanner = false;
            this.SetUpdateAppUserProfileCommand(isNewAvatar, isNewBanner);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(IdAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, isNewAvatar, isNewBanner, CancellationToken.None))
                .ReturnsAsync((bool?)null);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.UpdateStatus.Should().Be(AppUserProfileUpdateStatusEnum.ContentNotExist);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsNickNameIsUsed_WhenNickNameIsUsed()
        {
            this.SetUpdateAppUserProfileCommand(false, false);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(IdAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(true);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.UpdateStatus.Should().Be(AppUserProfileUpdateStatusEnum.NickNameIsUsed);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsNickNameIsUsed_WhenCheckIfNickNameIsUsedNotFindUser()
        {
            this.SetUpdateAppUserProfileCommand(false, false);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(IdAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync((bool?)null);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.UpdateStatus.Should().Be(AppUserProfileUpdateStatusEnum.ContentNotExist);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnUnSuccessful_WhenAvatarSaveIsNotSuccessful()
        {
            var isNewAvatar = true;
            var isNewBanner = false;
            this.SetUpdateAppUserProfileCommand(isNewAvatar, isNewBanner);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(IdAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.SetSaveImage(_avatarName, false);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.UpdateStatus.Should().Be(AppUserProfileUpdateStatusEnum.UnSuccessful);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnUnSuccessful_WhenBannerSaveIsNotSuccessful()
        {
            var isNewAvatar = false;
            var isNewBanner = true;
            this.SetUpdateAppUserProfileCommand(isNewAvatar, isNewBanner);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(IdAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.SetSaveImage(_bannerName, false);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.UpdateStatus.Should().Be(AppUserProfileUpdateStatusEnum.UnSuccessful);
            result.AvatarUrl.Should().BeNull();
        }

        private static UpdateAppUserProfileRequestDTO GetNewAppUserData(IFormFile? avatar, IFormFile? banner, bool isNewAvatar, bool isNewBanner)
        {
            return new UpdateAppUserProfileRequestDTO
            {
                NickName = "NickName",
                FirstName = "FirstName",
                LastName = "LastName",
                City = "City",
                AboutText = "AboutText",
                Birthdate = new DateTime(2001, 1, 1),
                PhoneNumber = "PhoneNumber",
                SemanticScholarProfile = "SemanticScholarProfile",
                Avatar = avatar,
                IsNewAvatar = isNewAvatar,
                Banner = banner,
                IsNewBanner = isNewBanner,
            };
        }

        private void SetUpdateAppUserProfileCommand(bool isNewAvatar, bool isNewBanner)
        {
            IFormFile? avatar = isNewAvatar ? GetImage("png") : null;
            IFormFile? banner = isNewBanner ? GetImage("png") : null;
            var newAppUserData = GetNewAppUserData(avatar, banner, isNewAvatar, isNewBanner);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, IdAppUser);
        }

        private static IFormFile GetImage(string contentType)
        {
            var file = new Mock<IFormFile>();
            file.Setup(f => f.ContentType).Returns(contentType);
            return file.Object;
        }

        private void SetSaveImage(string imageName, bool result)
        {
            this.imageService
                .Setup(i => i.SaveImageAsync(It.IsAny<IFormFile>(), imageName, It.IsAny<ImageFolder>(), CancellationToken.None))
                .ReturnsAsync(result);
        }
    }
}