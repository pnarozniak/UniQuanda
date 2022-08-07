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
using UniQuanda.Core.Domain.Entities;

namespace UniQuanda.Tests.CQRS.Commands.Auth.Login
{
    [TestFixture]
    public class LoginHandlerTests
    {
        private const string AccessToken = "AccessToken";
        private const string RefreshToken = "RefreshToken";
        private const string UserEmail = "email@email.com";
        private const string PlainPassword = "PlainPassword";
        private const string HashedPassword = "HashedPassword";
        private readonly DateTime _expirationRefreshTokne = new DateTime().AddDays(1);

        private LoginHandler loginHandler;
        private LoginCommand loginCommand;
        private Mock<IAuthRepository> authRepository;
        private Mock<IPasswordsService> passwordsService;
        private Mock<ITokensService> tokensService;

        [SetUp]
        public void SetupTests()
        {
            this.authRepository = new Mock<IAuthRepository>();
            this.passwordsService = new Mock<IPasswordsService>();
            this.tokensService = new Mock<ITokensService>();

            SetupLoginCommand();
            this.tokensService
                .Setup(ts => ts.GenerateAccessToken(It.IsAny<UserEntity>()))
                .Returns(AccessToken);
            this.tokensService
                .Setup(ts => ts.GenerateRefreshToken())
                .Returns(new Tuple<string, DateTime>(RefreshToken, _expirationRefreshTokne));

            this.loginHandler = new LoginHandler(this.authRepository.Object, this.passwordsService.Object, this.tokensService.Object);
        }

        [Test]
        public async Task Login_ShouldReturnTokensAndUserData_WhenCredentialsAreValid()
        {
            var userEntity = GetUserEntity();
            this.passwordsService
                .Setup(ps => ps.VerifyPassword(It.Is<string>(s => s == PlainPassword), It.Is<string>(s => s == HashedPassword)))
                .Returns(true);
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(It.Is<string>(s => s == UserEmail)))
                .ReturnsAsync(userEntity);
            this.authRepository
                .Setup(ar => ar.UpdateUserRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(true);

            var result = await loginHandler.Handle(this.loginCommand, CancellationToken.None);

            result.Status.Should().Be(LoginResponseDTO.LoginStatus.Success);
            result.AccessToken.Should().NotBeNull();
            result.RefreshToken.Should().NotBeNull();
            result.Nickname.Should().NotBeNull();
        }

        [Test]
        public async Task Login_ShouldReturnLoginStatusEqualInvalidCredentials_WhenUserIsNotFindedByEmail()
        {
            UserEntity? userEntity = null;
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(It.Is<string>(s => s == UserEmail)))
                .ReturnsAsync(userEntity);

            var result = await loginHandler.Handle(this.loginCommand, CancellationToken.None);

            result.Status.Should().Be(LoginResponseDTO.LoginStatus.InvalidCredentials);
        }

        [Test]
        public async Task Login_ShouldReturnLoginStatusEqualInvalidCredentials_WhenPasswordIsIncorrect()
        {
            var userEntity = GetUserEntity();
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(It.Is<string>(s => s == UserEmail)))
                .ReturnsAsync(userEntity);
            this.passwordsService
                .Setup(ps => ps.VerifyPassword(It.Is<string>(s => s == PlainPassword), It.Is<string>(s => s == HashedPassword)))
                .Returns(false);

            var result = await loginHandler.Handle(this.loginCommand, CancellationToken.None);

            result.Status.Should().Be(LoginResponseDTO.LoginStatus.InvalidCredentials);
        }

        [Test]
        public async Task Login_ShouldReturnLoginStatusEqualEmailNotConfirmed_WhenUserEmailIsNotConfirmed()
        {
            var userEntity = GetUserEntity(false);
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(It.Is<string>(s => s == UserEmail)))
                .ReturnsAsync(userEntity);
            this.passwordsService
                .Setup(ps => ps.VerifyPassword(It.Is<string>(s => s == PlainPassword), It.Is<string>(s => s == HashedPassword)))
                .Returns(true);

            var result = await loginHandler.Handle(this.loginCommand, CancellationToken.None);

            result.Status.Should().Be(LoginResponseDTO.LoginStatus.EmailNotConfirmed);
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
                Nickname = "Nickname",
                HashedPassword = HashedPassword,
                Emails = new List<string>() { UserEmail },
                IsEmailConfirmed = isEmailConfirmed
            };
        }
    }
}