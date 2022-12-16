using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Register;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;

namespace UniQuanda.Tests.CQRS.Commands.Auth.Register
{
    [TestFixture]
    public class RegisterHandlerTests
    {
        private const string EmailConfirmationToken = "ABC123";
        private const string PlainPassword = "PlainPassword";
        private const string HashedPassword = "HashedPassword";

        private RegisterHandler registerHandler;
        private RegisterCommand registerCommand;
        private Mock<IAuthRepository> authRepository;
        private Mock<ITokensService> tokensService;
        private Mock<IEmailService> emailService;
        private Mock<IPasswordsService> passwordsService;
        private Mock<IExpirationService> expirationService;

        [SetUp]
        public void SetupTests()
        {
            this.authRepository = new Mock<IAuthRepository>();
            this.tokensService = new Mock<ITokensService>();
            this.emailService = new Mock<IEmailService>();
            this.passwordsService = new Mock<IPasswordsService>();
            this.expirationService = new Mock<IExpirationService>();

            SetupRegisterCommand();
            this.tokensService
                .Setup(ts => ts.GenerateEmailConfirmationToken())
                .Returns(EmailConfirmationToken);
            this.passwordsService
                .Setup(ps => ps.HashPassword(PlainPassword))
                .Returns(HashedPassword);
            this.expirationService
                .Setup(es => es.GetNewUserExpirationInHours())
                .Returns(7);

            this.registerHandler = new RegisterHandler(this.authRepository.Object, this.tokensService.Object, this.emailService.Object, this.passwordsService.Object, this.expirationService.Object);
        }

        [Test]
        public async Task Register_ShouldReturnTrue_WhenRequestDataAreValid()
        {
            this.authRepository
                .Setup(ar => ar.RegisterNewUserAsync(It.IsAny<NewUserEntity>(), CancellationToken.None))
                .ReturnsAsync(true);

            var result = await registerHandler.Handle(this.registerCommand, CancellationToken.None);

            result.Should().Be(true);
        }

        [Test]
        public async Task Register_ShouldReturnFalse_WhenUserIsAlreadyRegistered()
        {
            this.authRepository
                .Setup(ar => ar.RegisterNewUserAsync(It.IsAny<NewUserEntity>(), CancellationToken.None))
                .ReturnsAsync(false);

            var result = await registerHandler.Handle(this.registerCommand, CancellationToken.None);

            result.Should().Be(false);
        }

        private void SetupRegisterCommand()
        {
            var registerRequestDTO = new RegisterRequestDTO()
            {
                Nickname = "Nickname",
                Email = "email@email.com",
                Password = PlainPassword,
                FirstName = "FirstName",
                LastName = "LastName",
                Birthdate = new DateTime(2000, 1, 1),
                Contact = "123456789",
                City = "City"
            };
            this.registerCommand = new(registerRequestDTO);
        }
    }
}
