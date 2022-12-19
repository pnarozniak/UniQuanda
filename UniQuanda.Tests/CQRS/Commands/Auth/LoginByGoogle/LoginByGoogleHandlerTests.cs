using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.LoginByGoogle;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Tests.CQRS.Commands.Auth.LoginByGoogle
{
    [TestFixture]
    public class LoginByGoogleHandlerTests
    {
        private LoginByGoogleHandler loginByGoogleHandler;
        private LoginByGoogleCommand loginByGoogleCommand;
        private Mock<IAuthRepository> authRepository;
        private Mock<ITokensService> tokensService;
        private Mock<IOAuthService> oauthService;
        private Mock<IRoleRepository> roleRepository;

        private string accessToken = "access-token";
        private GoogleIdToken googleIdToken = new GoogleIdToken
        {
            Id = "id",
            Email = "email",
        };
        private string url = "url";
        private Tuple<string, DateTime> refreshToken = new("", new DateTime());

        [SetUp]
        public void SetupTests()
        {
            this.authRepository = new Mock<IAuthRepository>();
            this.tokensService = new Mock<ITokensService>();
            this.oauthService = new Mock<IOAuthService>();
            this.roleRepository = new Mock<IRoleRepository>();

            this.tokensService
                .Setup(ts => ts.GenerateAccessToken(It.IsAny<int>(), It.IsAny<IEnumerable<AppRoleEntity>>(), It.IsAny<IEnumerable<AuthRole>>()))
                .Returns(accessToken);
            this.oauthService
                .Setup(os => os.GetGoogleIdTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(googleIdToken);
            this.oauthService
                .Setup(os => os.GetGoogleClientHandlerUrl())
                .Returns(url);
            this.tokensService
                    .Setup(ts => ts.GenerateRefreshToken())
                    .Returns(this.refreshToken);

            this.loginByGoogleCommand = new LoginByGoogleCommand(new LoginByGoogleRequestDTO { Code = "Code" });
            this.loginByGoogleHandler = new LoginByGoogleHandler(this.authRepository.Object, this.tokensService.Object, this.oauthService.Object, roleRepository.Object);
        }

        [Test]
        public async Task LoginByGoogle_ShouldReturnUrlWithError500_WhenGoogleIdTokenIsInvalid()
        {
            GoogleIdToken? googleIdToken = null;
            this.oauthService
                .Setup(os => os.GetGoogleIdTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(googleIdToken);

            var result = await this.loginByGoogleHandler.Handle(this.loginByGoogleCommand, CancellationToken.None);
            result.Should().Be(url + "?error=500");
        }

        [Test]
        public async Task LoginByGoogle_ShouldReturnUrlWithError409_WhenUserWithEmailExistsButIsNotOAuthUser()
        {
            var userEntity = new UserEntity
            {
                Id = 1,
                IsOAuthUser = false
            };
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(userEntity);

            var result = await this.loginByGoogleHandler.Handle(this.loginByGoogleCommand, CancellationToken.None);
            result.Should().Be(url + "?error=409");
        }

        [Test]
        public async Task LoginByGoogle_ShouldReturnUrlWithConfirmationCode_WhenOAuthReigstrationIsNotConfirmed()
        {
            var userEntity = new UserEntity
            {
                Id = 1,
                IsOAuthUser = true,
                IsOAuthRegisterCompleted = false
            };
            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(userEntity);

            var result = await this.loginByGoogleHandler.Handle(this.loginByGoogleCommand, CancellationToken.None);
            result.Should().Be(url + "?confirmationCode=" + this.loginByGoogleCommand.Code);
        }

        [Test]
        public async Task LoginByGoogle_ShouldReturnUrlWithAccessToken_WhenLoginIsSuccessful()
        {
            var userEntity = new UserEntity
            {
                Id = 1,
                IsOAuthUser = true,
                IsOAuthRegisterCompleted = true
            };

            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(userEntity);

            var result = await this.loginByGoogleHandler.Handle(this.loginByGoogleCommand, CancellationToken.None);
            result.Should().Be(url + "?accessToken=" + this.accessToken);
        }

        [Test]
        public async Task LoginByGoogle_ShouldReturnUrlWithError409_WhenEmailIsNotRegisteredAndAnErrorOccurresDuringOAuthRegistration()
        {
            UserEntity? userEntity = null;

            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(userEntity);

            this.authRepository
                .Setup(ar => ar.RegisterOAuthUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<OAuthProviderEnum>(), CancellationToken.None))
                .ReturnsAsync(false);

            var result = await this.loginByGoogleHandler.Handle(this.loginByGoogleCommand, CancellationToken.None);
            result.Should().Be(url + "?error=409");
        }

        [Test]
        public async Task LoginByGoogle_ShouldReturnUrlWithConfirmationCode_WhenEmailIsNotRegisteredAndAnOAuthRegistrationIsSuccessful()
        {
            UserEntity? userEntity = null;

            this.authRepository
                .Setup(ar => ar.GetUserByEmailAsync(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(userEntity);

            this.authRepository
                .Setup(ar => ar.RegisterOAuthUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<OAuthProviderEnum>(), CancellationToken.None))
                .ReturnsAsync(true);

            var result = await this.loginByGoogleHandler.Handle(this.loginByGoogleCommand, CancellationToken.None);
            result.Should().Be(url + "?confirmationCode=" + this.loginByGoogleCommand.Code);
        }
    }
}