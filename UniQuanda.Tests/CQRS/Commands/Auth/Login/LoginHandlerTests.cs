using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Login;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Tests.CQRS.Commands.Auth.Login
{
    [TestFixture]
    public class LoginHandlerTests
    {
        private const string AccessToken = "AccessToken";
        private const string RefreshToken = "RefreshToken";
        private const string UserEmail = "email@email.com";
        private const string Nickname = "Nickname";
        private const string Avatar = "AvatarUrl";
        private const string PlainPassword = "PlainPassword";
        private const string HashedPassword = "HashedPassword";
        private readonly DateTime _expirationRefreshToken = new DateTime().AddDays(1);

        private LoginHandler loginHandler;
        private LoginCommand loginCommand;
        private Mock<IAuthRepository> authRepository;
        private Mock<IPasswordsService> passwordsService;
        private Mock<ITokensService> tokensService;
        private Mock<IAppUserRepository> appUserProfileRepository;

        [SetUp]
        public void SetupTests()
        {
            this.authRepository = new Mock<IAuthRepository>();
            this.passwordsService = new Mock<IPasswordsService>();
            this.tokensService = new Mock<ITokensService>();
            this.appUserProfileRepository = new Mock<IAppUserRepository>();

            SetupLoginCommand();
            this.tokensService
                .Setup(ts => ts.GenerateAccessToken(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<bool>()))
                .Returns(AccessToken);
            this.tokensService
                .Setup(ts => ts.GenerateRefreshToken())
                .Returns(new Tuple<string, DateTime>(RefreshToken, _expirationRefreshToken));
            this.appUserProfileRepository
                .Setup(aur => aur.GetUserAvatarAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Avatar);

            this.loginHandler = new LoginHandler(this.authRepository.Object, this.passwordsService.Object, this.tokensService.Object, this.appUserProfileRepository.Object);
        }

        [Test]
        public async Task Login_ShouldReturnTokensAndUserData_WhenCredentialsAreValid()
        {
            var userEntity = GetUserEntity();
            this.passwordsService
                .Setup(ps => ps.VerifyPassword(PlainPassword, HashedPassword))
                .Returns(true);
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(UserEmail, CancellationToken.None))
                .ReturnsAsync(userEntity);
            this.authRepository
                .Setup(ar => ar.UpdateUserRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>(), CancellationToken.None))
                .ReturnsAsync(true);

            var result = await loginHandler.Handle(this.loginCommand, CancellationToken.None);

            result.Status.Should().Be(LoginResponseDTO.LoginStatus.Success);
            result.AccessToken.Should().Be(AccessToken);
            result.RefreshToken.Should().Be(RefreshToken);
            result.Nickname.Should().Be(Nickname);
            result.Avatar.Should().Be(Avatar);
        }

        [Test]
        public async Task Login_ShouldReturnTokensAndUserDataWithoutAvatar_WhenCredentialsAreValidAndUserHasNoAvatar()
        {
            var userEntity = GetUserEntity();
            this.passwordsService
                .Setup(ps => ps.VerifyPassword(PlainPassword, HashedPassword))
                .Returns(true);
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(UserEmail, CancellationToken.None))
                .ReturnsAsync(userEntity);
            this.authRepository
                .Setup(ar => ar.UpdateUserRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>(), CancellationToken.None))
                .ReturnsAsync(true);
            this.appUserProfileRepository
                .Setup(aur => aur.GetUserAvatarAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as string);

            var result = await loginHandler.Handle(this.loginCommand, CancellationToken.None);

            result.Status.Should().Be(LoginResponseDTO.LoginStatus.Success);
            result.AccessToken.Should().Be(AccessToken);
            result.RefreshToken.Should().Be(RefreshToken);
            result.Nickname.Should().Be(Nickname);
            result.Avatar.Should().BeNull();
        }

        [Test]
        public async Task Login_ShouldReturnLoginStatusEqualInvalidCredentials_WhenUserIsNotFindedByEmail()
        {
            UserEntity? userEntity = null;
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(UserEmail, CancellationToken.None))
                .ReturnsAsync(userEntity);

            var result = await loginHandler.Handle(this.loginCommand, CancellationToken.None);

            result.Status.Should().Be(LoginResponseDTO.LoginStatus.InvalidCredentials);
            result.AccessToken.Should().BeNull();
            result.RefreshToken.Should().BeNull();
            result.Nickname.Should().BeNull();
            result.Avatar.Should().BeNull();
        }

        [Test]
        public async Task Login_ShouldReturnLoginStatusEqualInvalidCredentials_WhenPasswordIsIncorrect()
        {
            var userEntity = GetUserEntity();
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(UserEmail, CancellationToken.None))
                .ReturnsAsync(userEntity);
            this.passwordsService
                .Setup(ps => ps.VerifyPassword(PlainPassword, HashedPassword))
                .Returns(false);

            var result = await loginHandler.Handle(this.loginCommand, CancellationToken.None);

            result.Status.Should().Be(LoginResponseDTO.LoginStatus.InvalidCredentials);
            result.AccessToken.Should().BeNull();
            result.RefreshToken.Should().BeNull();
            result.Nickname.Should().BeNull();
            result.Avatar.Should().BeNull();
        }

        [Test]
        public async Task Login_ShouldReturnLoginStatusEqualEmailNotConfirmed_WhenUserEmailIsNotConfirmed()
        {
            var userEntity = GetUserEntity(false);
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(UserEmail, CancellationToken.None))
                .ReturnsAsync(userEntity);
            this.passwordsService
                .Setup(ps => ps.VerifyPassword(PlainPassword, HashedPassword))
                .Returns(true);

            var result = await loginHandler.Handle(this.loginCommand, CancellationToken.None);

            result.Status.Should().Be(LoginResponseDTO.LoginStatus.EmailNotConfirmed);
            result.AccessToken.Should().BeNull();
            result.RefreshToken.Should().BeNull();
            result.Nickname.Should().BeNull();
            result.Avatar.Should().BeNull();
        }

        private void SetupLoginCommand()
        {
            var loginRequestDTO = new LoginRequestDTO()
            {
                Email = UserEmail,
                Password = PlainPassword
            };
            this.loginCommand = new(loginRequestDTO);
        }

        private static UserEntity GetUserEntity(bool isEmailConfirmed = true)
        {
            return new()
            {
                Id = 1,
                Nickname = Nickname,
                HashedPassword = HashedPassword,
                Emails = new List<string>() { UserEmail },
                IsEmailConfirmed = isEmailConfirmed,
                HasPremiumUntil = DateTime.UtcNow.AddMonths(1),
                OptionalInfo = new UserOptionalInfo
                {
                    Avatar = Avatar
                }
            };
        }
    }
}