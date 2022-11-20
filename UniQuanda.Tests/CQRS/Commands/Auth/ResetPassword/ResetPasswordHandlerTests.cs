using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ResetPasword;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Tests.CQRS.Commands.Auth.ResetPassword
{
    [TestFixture]
    public class ResetPasswordHandlerTests
    {
        private const string PasswordRecoveryToken = "PasswordRecoveryToken";
        private const string Email = "test@gmail.com";
        private const string NewPassword = "my-new-password";
        private const string HashedNewPassword = "my-new-password-is-now-hashed";
        private const int IdUser = 1;

        private ResetPasswordHandler resetPasswordHandler;
        private ResetPasswordCommand resetPasswordCommand;
        private Mock<IAuthRepository> authRepository;
        private Mock<IEmailService> emailService;
        private Mock<IPasswordsService> passwordsService;

        [SetUp]
        public void SetupTests()
        {
            authRepository = new Mock<IAuthRepository>();
            emailService = new Mock<IEmailService>();
            passwordsService = new Mock<IPasswordsService>();

            passwordsService
                    .Setup(es => es.HashPassword(NewPassword))
                    .Returns(HashedNewPassword);

            authRepository
                    .Setup(ar => ar.ResetUserPasswordAsync(
                            It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), CancellationToken.None
                    )).ReturnsAsync(true);

            resetPasswordCommand = new ResetPasswordCommand(new ResetPaswordDTO()
            {
                Email = Email,
                RecoveryToken = PasswordRecoveryToken,
                NewPassword = NewPassword
            }, new UserAgentInfo(){});
            resetPasswordHandler = new ResetPasswordHandler(authRepository.Object, emailService.Object, passwordsService.Object);
        }

        [Test]
        public async Task ResetPassword_ShouldReturnFalse_WhenUserNotExists()
        {
            UserEntity? userEntity = null;
            authRepository
                    .Setup(ar => ar.GetUserByEmailAsync(Email, CancellationToken.None))
                    .ReturnsAsync(userEntity);

            var result = await resetPasswordHandler.Handle(this.resetPasswordCommand, CancellationToken.None);
            result.Should().BeFalse();
        }

        [Test]
        public async Task ResetPassword_ShouldReturnFalse_WhenUserExistsButDoNotHaveConfirmedEmail()
        {
            var userEntity = GetUserEntity(false);
            authRepository
                    .Setup(ar => ar.GetUserByEmailAsync(Email, CancellationToken.None))
                    .ReturnsAsync(userEntity);
            var result = await resetPasswordHandler.Handle(this.resetPasswordCommand, CancellationToken.None);
            result.Should().BeFalse();
        }

        [Test]
        public async Task ResetPassword_ShouldReturnFalse_WhenUserHaveConfirmedEmailButPasswordRecoveryActionNotExists()
        {
            var userEntity = GetUserEntity(true);
            UserActionToConfirmEntity? recoveryAction = null;
            authRepository
                    .Setup(ar => ar.GetUserByEmailAsync(Email, CancellationToken.None))
                    .ReturnsAsync(userEntity);

            authRepository
                    .Setup(ar => ar.GetUserActionToConfirmAsync(
                        UserActionToConfirmEnum.RecoverPassword, PasswordRecoveryToken, CancellationToken.None
                    )).ReturnsAsync(recoveryAction);

            var result = await resetPasswordHandler.Handle(this.resetPasswordCommand, CancellationToken.None);
            result.Should().BeFalse();
        }

        [Test]
        public async Task ResetPassword_ShouldReturnFalse_WhenUserHaveConfirmedEmailAndPasswordRecoveryActionExistsButHasExpired()
        {
            var userEntity = GetUserEntity(true);
            var recoveryAction = GetUserActionToConfirmEntity(IdUser, DateTime.UtcNow.AddMinutes(-30));
            authRepository
                    .Setup(ar => ar.GetUserByEmailAsync(Email, CancellationToken.None))
                    .ReturnsAsync(userEntity);

            authRepository
                    .Setup(ar => ar.GetUserActionToConfirmAsync(
                        UserActionToConfirmEnum.RecoverPassword, PasswordRecoveryToken, CancellationToken.None
                    )).ReturnsAsync(recoveryAction);

            var result = await resetPasswordHandler.Handle(this.resetPasswordCommand, CancellationToken.None);
            result.Should().BeFalse();
        }

        [Test]
        public async Task ResetPassword_ShouldReturnFalse_WhenUserHaveConfirmedEmailAndPasswordRecoveryActionExistsAndIsValidButBelongsToDifferentUser()
        {
            var userEntity = GetUserEntity(true);
            var recoveryAction = GetUserActionToConfirmEntity(IdUser + 1, DateTime.UtcNow.AddMinutes(30));
            authRepository
                    .Setup(ar => ar.GetUserByEmailAsync(Email, CancellationToken.None))
                    .ReturnsAsync(userEntity);

            authRepository
                    .Setup(ar => ar.GetUserActionToConfirmAsync(
                        UserActionToConfirmEnum.RecoverPassword, PasswordRecoveryToken, CancellationToken.None
                    )).ReturnsAsync(recoveryAction);

            var result = await resetPasswordHandler.Handle(this.resetPasswordCommand, CancellationToken.None);
            result.Should().BeFalse();
        }

        [Test]
        public async Task ResetPassword_ShouldReturnTrue_WhenUserHaveConfirmedEmailAndPasswordRecoveryActionIsFoundAndIsNotExpired()
        {
            var userEntity = GetUserEntity(true);
            var recoveryAction = GetUserActionToConfirmEntity(IdUser, DateTime.UtcNow.AddMinutes(30));
            authRepository
                    .Setup(ar => ar.GetUserByEmailAsync(Email, CancellationToken.None))
                    .ReturnsAsync(userEntity);

            authRepository
                    .Setup(ar => ar.GetUserActionToConfirmAsync(
                        UserActionToConfirmEnum.RecoverPassword, PasswordRecoveryToken, CancellationToken.None
                    )).ReturnsAsync(recoveryAction);

            var result = await resetPasswordHandler.Handle(this.resetPasswordCommand, CancellationToken.None);
            result.Should().BeTrue();
        }

        private static UserEntity GetUserEntity(bool isEmailConfirmed = true)
        {
            return new()
            {
                Id = IdUser,
                Nickname = "",
                HashedPassword = "",
                Emails = new List<string>() { Email },
                IsEmailConfirmed = isEmailConfirmed,
                OptionalInfo = new UserOptionalInfo(),
            };
        }

        private static UserActionToConfirmEntity GetUserActionToConfirmEntity(int idUser, DateTime existsUntil)
        {
            return new()
            {
                Id = 1,
                IdUser = idUser,
                ConfirmationToken = PasswordRecoveryToken,
                ActionType = UserActionToConfirmEnum.RecoverPassword,
                ExistsUntil = existsUntil
            };
        }
    }
}