using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmOAuthRegister;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Tests.CQRS.Commands.Auth.ConfirmOAuthRegister
{
    [TestFixture]
    public class ConfirmOAuthRegisterHandlerTests
    {
        private ConfirmOAuthRegisterHandler confirmOAuthRegisterHandler;
        private ConfirmOAuthRegisterCommand confirmOAuthRegisterCommand;
        private Mock<IAuthRepository> authRepository;
        private Mock<ITokensService> tokensService;
        private Mock<IEmailService> emailService;
        private Mock<IUniversityRepository> universityRepository;

        private string accessToken = "access-token";
        private const int UniversityId = 1;
        private const string UniversityRegex = "domain.com";

        [SetUp]
        public void SetupTests()
        {
            this.authRepository = new Mock<IAuthRepository>();
            this.tokensService = new Mock<ITokensService>();
            this.emailService = new Mock<IEmailService>();
            this.universityRepository = new Mock<IUniversityRepository>();

            this.tokensService
                .Setup(ts => ts.GenerateAccessToken(It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(accessToken);

            this.universityRepository
                .Setup(ur => ur.GetUniversitiresWhereUserIsNotPresentAsync(It.IsAny<int>(), CancellationToken.None))
                .ReturnsAsync(new List<UniversityEntity>() { new UniversityEntity()
                    {
                        Id = UniversityId,
                        Regex = UniversityRegex
                    }
                });

            this.confirmOAuthRegisterCommand = new ConfirmOAuthRegisterCommand(new ConfirmOAuthRegisterRequestDTO());
            this.confirmOAuthRegisterHandler = new ConfirmOAuthRegisterHandler(this.authRepository.Object, this.tokensService.Object, this.emailService.Object, this.universityRepository.Object);
        }

        [Test]
        public async Task ConfirmOAuthRegister_ShouldReturnNull_WhenErrorOccurresDuringRegisterConfirmation()
        {
            int? userId = null;
            this.authRepository
                .Setup(ar => ar.ConfirmOAuthRegisterAsync(It.IsAny<string>(), It.IsAny<NewUserEntity>(), CancellationToken.None))
                .ReturnsAsync(userId);

            var result = await this.confirmOAuthRegisterHandler.Handle(this.confirmOAuthRegisterCommand, CancellationToken.None);
            result.Should().Be(null);
        }


        [Test]
        public async Task ConfirmOAuthRegister_ShouldReturnDTOWithAccessToken_WhenRegisterConfirmationIsSuccessful()
        {
            int? userId = 1;
            this.authRepository
                .Setup(ar => ar.ConfirmOAuthRegisterAsync(It.IsAny<string>(), It.IsAny<NewUserEntity>(), CancellationToken.None))
                .ReturnsAsync(userId);

            this.authRepository
                .Setup(ar => ar.GetUserWithEmailsByIdAsync(It.IsAny<int>(), CancellationToken.None))
                .ReturnsAsync(
                    new UserSecurityEntity()
                    {
                        Emails = new List<UserEmailSecurity>
                    {
                                    new() { IsMain = true, Value = "" }
                    }
                    });

            var result = await this.confirmOAuthRegisterHandler.Handle(this.confirmOAuthRegisterCommand, CancellationToken.None);
            result.Should().BeEquivalentTo(
                new ConfirmOAuthRegisterResponseDTO { AccessToken = this.accessToken }
            );
        }
    }
}