using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using UniQuanda.Core.Application.CQRS.Commands.Auth.RecoverPassword;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Tests.CQRS.Commands.Auth.RecoverPassword
{
		[TestFixture]
		public class RecoverPasswordHandlerTests
		{
				private const string PasswordRecoveryToken = "PasswordRecoveryToken";
				private const int PasswordRecoveryActionExpirationInMinutes = 30;
				private const string Email = "test@gmail.com";

				private RecoverPasswordHandler recoverPasswordHandler;
				private RecoverPasswordCommand recoverPasswordCommand;
				private Mock<IAuthRepository> authRepository;
				private Mock<IEmailService> emailService;
				private Mock<IExpirationService> expirationService;
				private Mock<ITokensService> tokensService;

				[SetUp]
				public void SetupTests() {
						authRepository = new Mock<IAuthRepository>();
						emailService = new Mock<IEmailService>();
						expirationService = new Mock<IExpirationService>();
						tokensService = new Mock<ITokensService>();

						tokensService
								.Setup(ts => ts.GeneratePasswordRecoveryToken())
								.Returns(PasswordRecoveryToken);

						expirationService
								.Setup(es => es.GetRecoverPasswordActionExpirationInMinutes())
								.Returns(PasswordRecoveryActionExpirationInMinutes);

						authRepository
								.Setup(ar => ar.CreateUserActionToConfirmAsync(
										It.IsAny<int>(), UserActionToConfirmEnum.RECOVER_PASSWORD, 
										PasswordRecoveryToken, It.IsAny<DateTime>(), CancellationToken.None))
								.ReturnsAsync(true);

						recoverPasswordCommand = new RecoverPasswordCommand(new RecoverPasswordDTO() { Email = Email });
						recoverPasswordHandler = new RecoverPasswordHandler(authRepository.Object, emailService.Object, tokensService.Object, expirationService.Object);
				}

				[Test]
				public async Task RecoverPassword_ShouldReturnTrue_WhenUserExistsAndHaveConfirmedEmail() {
						var userEntity = GetUserEntity(true);
						authRepository
								.Setup(ar => ar.GetUserByEmailAsync(Email, CancellationToken.None))
								.ReturnsAsync(userEntity);

						var result = await recoverPasswordHandler.Handle(this.recoverPasswordCommand, CancellationToken.None);
						result.Should().BeTrue();
				} 

				[Test]
				public async Task RecoverPassword_ShouldReturnFalse_WhenUserNotExists() {
						UserEntity? userEntity = null;
						authRepository
								.Setup(ar => ar.GetUserByEmailAsync(Email, CancellationToken.None))
								.ReturnsAsync(userEntity);

						var result = await recoverPasswordHandler.Handle(this.recoverPasswordCommand, CancellationToken.None);
						result.Should().BeFalse();
				} 

				[Test]
				public async Task RecoverPassword_ShouldReturnFalse_WhenUserExistsButDoNotHaveConfirmedEmail() {
						var userEntity = GetUserEntity(false);
						authRepository
								.Setup(ar => ar.GetUserByEmailAsync(Email, CancellationToken.None))
								.ReturnsAsync(userEntity);

						var result = await recoverPasswordHandler.Handle(this.recoverPasswordCommand, CancellationToken.None);
						result.Should().BeFalse();
				} 

				private static UserEntity GetUserEntity(bool isEmailConfirmed = true)
        {
            return new()
            {
                Id = 1,
                Nickname = "",
                HashedPassword = "",
                Emails = new List<string>() { Email },
                IsEmailConfirmed = isEmailConfirmed,
								OptionalInfo = new UserOptionalInfo()
            };
        }
		}
}