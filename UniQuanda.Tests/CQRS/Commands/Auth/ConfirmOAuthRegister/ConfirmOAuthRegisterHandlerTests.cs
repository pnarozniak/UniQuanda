using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmOAuthRegister;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Login;
using UniQuanda.Core.Application.CQRS.Commands.Auth.LoginByGoogle;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Utils;
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

				private string accessToken = "access-token";

        [SetUp]
        public void SetupTests()
        {
            this.authRepository = new Mock<IAuthRepository>();
            this.tokensService = new Mock<ITokensService>();
						this.emailService = new Mock<IEmailService>();

            this.tokensService
                .Setup(ts => ts.GenerateAccessToken(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(accessToken);

						this.confirmOAuthRegisterCommand = new ConfirmOAuthRegisterCommand(new ConfirmOAuthRegisterRequestDTO());
            this.confirmOAuthRegisterHandler = new ConfirmOAuthRegisterHandler(this.authRepository.Object, this.tokensService.Object, this.emailService.Object);
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
							new ConfirmOAuthRegisterResponseDTO{ AccessToken = this.accessToken }
						);
				}
    }
}