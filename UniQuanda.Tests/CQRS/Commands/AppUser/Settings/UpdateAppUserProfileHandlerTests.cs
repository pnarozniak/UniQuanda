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
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Tests.CQRS.Commands.AppUser.Settings
{
    [TestFixture]
    public class UpdateAppUserProfileHandlerTests
    {
        private UpdateAppUserProfileHandler updateAppUserProfileHandler;
        private UpdateAppUserProfileCommand updateAppUserProfileCommand;
        private Mock<IAppUserRepository> appUserRepository;
        private Mock<IImageService> imageService;

        private readonly int _idAppUser = 1;
        private readonly string _avatarUrl = "AvatarUrl";
        [SetUp]
        public void SetupTests()
        {
            this.appUserRepository = new Mock<IAppUserRepository>();
            this.imageService = new Mock<IImageService>();
            this.updateAppUserProfileHandler = new UpdateAppUserProfileHandler(this.appUserRepository.Object, this.imageService.Object);
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsSuccessful_WhenUpdateIsSuccessfulAndContainsAvatarAndBanner()
        {
            var avatar = GetImage("png");
            var banner = GetImage("png");
            var newAppUserData = GetNewAppUserData(avatar, banner);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(_idAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(true, this._avatarUrl));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.AppUserUpdateStatus.Should().Be(AppUserUpdateStatusEnum.Successful);
            result.AvatarUrl.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsSuccessful_WhenUpdateIsSuccessfulAndContainsAvatar()
        {
            var avatar = GetImage("png");
            var newAppUserData = GetNewAppUserData(avatar, null);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(_idAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(true, this._avatarUrl));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.AppUserUpdateStatus.Should().Be(AppUserUpdateStatusEnum.Successful);
            result.AvatarUrl.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsSuccessful_WhenUpdateIsSuccessfulAndContainsBanner()
        {
            var banner = GetImage("png");
            var newAppUserData = GetNewAppUserData(null, banner);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(_idAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(true, null));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.AppUserUpdateStatus.Should().Be(AppUserUpdateStatusEnum.Successful);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsSuccessful_WhenUpdateIsSuccessfulAndNotContainsAvatarAndBanner()
        {
            var newAppUserData = GetNewAppUserData(null, null);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(_idAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(true, null));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.AppUserUpdateStatus.Should().Be(AppUserUpdateStatusEnum.Successful);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsNotSuccessful_WhenUpdateAppUserIsNotSuccessful()
        {
            var newAppUserData = GetNewAppUserData(null, null);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(_idAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(false, null));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.AppUserUpdateStatus.Should().Be(AppUserUpdateStatusEnum.NotSuccessful);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsAppUserNotExist_WhenAppUserIsNotFound()
        {
            var newAppUserData = GetNewAppUserData(null, null);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(_idAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(false);
            this.appUserRepository
                .Setup(a => a.UpdateAppUserAsync(updateAppUserProfileCommand.AppUser, CancellationToken.None))
                .ReturnsAsync(GetAppUserProfileUpdateResult(null, null));

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.AppUserUpdateStatus.Should().Be(AppUserUpdateStatusEnum.AppUserNotExist);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsNickNameIsUsed_WhenNickNameIsUsed()
        {
            var newAppUserData = GetNewAppUserData(null, null);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(_idAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync(true);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.AppUserUpdateStatus.Should().Be(AppUserUpdateStatusEnum.NickNameIsUsed);
            result.AvatarUrl.Should().BeNull();
        }

        [Test]
        public async Task UpdateAppUserProfile_ShouldReturnStatusAsNickNameIsUsed_WhenCheckIfNickNameIsUsedNotFindUser()
        {
            var newAppUserData = GetNewAppUserData(null, null);
            this.updateAppUserProfileCommand = new UpdateAppUserProfileCommand(newAppUserData, _idAppUser);
            this.appUserRepository
                .Setup(ar => ar.IsNicknameUsedAsync(_idAppUser, updateAppUserProfileCommand.AppUser.Nickname, CancellationToken.None))
                .ReturnsAsync((bool?)null);

            var result = await updateAppUserProfileHandler.Handle(this.updateAppUserProfileCommand, CancellationToken.None);

            result.AppUserUpdateStatus.Should().Be(AppUserUpdateStatusEnum.AppUserNotExist);
            result.AvatarUrl.Should().BeNull();
        }

        private static UpdateAppUserProfileRequestDTO GetNewAppUserData(IFormFile? avatar, IFormFile? banner)
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
                Banner = banner
            };
        }

        private static IFormFile GetImage(string contentType)
        {
            var file = new Mock<IFormFile>();
            file.Setup(f => f.ContentType).Returns(contentType);
            return file.Object;
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