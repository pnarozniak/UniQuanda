using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ResendRegisterConfirmationCode;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;

namespace UniQuanda.Tests.CQRS.Commands.Auth.ResendRegisterConfirmationCode
{
    [TestFixture]
    public class ResendRegisterConfirmationCodeHandlerTests
    {
        private const string EmailConfirmationToken = "ABC123";
        private const string UserEmail = "email@email.com";

        private ResendRegisterConfirmationCodeHandler resendRegisterConfirmationCodeHandler;
        private ResendRegisterConfirmationCodeCommand resendRegisterConfirmationCodeCommand;
        private Mock<IAuthRepository> authRepository;
        private Mock<IEmailService> emailService;
        private Mock<ITokensService> tokensService;

        [SetUp]
        public void SetupTests()
        {
            this.authRepository = new Mock<IAuthRepository>();
            this.emailService = new Mock<IEmailService>();
            this.tokensService = new Mock<ITokensService>();

            SetupResendRegisterConfirmationCodeCommand();
            this.tokensService
                .Setup(ts => ts.GenerateEmailConfirmationToken())
                .Returns(EmailConfirmationToken);

            this.resendRegisterConfirmationCodeHandler = new ResendRegisterConfirmationCodeHandler(this.authRepository.Object, this.emailService.Object, this.tokensService.Object);
        }

        [Test]
        public async Task ResendRegisterConfirmationCode_ShouldReturnTrue_WhenUpdateIsSuccesful()
        {
            this.authRepository
                .Setup(ar => ar.UpdateTempUserEmailConfirmationCodeAsync(UserEmail, EmailConfirmationToken, CancellationToken.None))
                .ReturnsAsync(true);

            var result = await resendRegisterConfirmationCodeHandler.Handle(this.resendRegisterConfirmationCodeCommand, CancellationToken.None);

            result.Should().Be(true);
        }
        [Test]
        public async Task ResendRegisterConfirmationCode_ShouldReturnFalse_WhenTempUserNotFinded()
        {
            this.authRepository
                .Setup(ar => ar.UpdateTempUserEmailConfirmationCodeAsync(UserEmail, EmailConfirmationToken, CancellationToken.None))
                .ReturnsAsync((bool?)null);

            var result = await resendRegisterConfirmationCodeHandler.Handle(this.resendRegisterConfirmationCodeCommand, CancellationToken.None);

            result.Should().Be(false);
        }

        [Test]
        public async Task ResendRegisterConfirmationCode_ShouldReturnFalse_WhenUpdateEmailConfirmationCodeIsNotSuccessful()
        {
            this.authRepository
                .Setup(ar => ar.UpdateTempUserEmailConfirmationCodeAsync(UserEmail, EmailConfirmationToken, CancellationToken.None))
                .ReturnsAsync(false);

            var result = await resendRegisterConfirmationCodeHandler.Handle(this.resendRegisterConfirmationCodeCommand, CancellationToken.None);

            result.Should().Be(false);
        }

        private void SetupResendRegisterConfirmationCodeCommand()
        {
            var resendRegisterConfirmationCodeRequestDto = new ResendRegisterConfirmationCodeRequestDTO()
            {
                Email = UserEmail
            };
            this.resendRegisterConfirmationCodeCommand = new(resendRegisterConfirmationCodeRequestDto);
        }
    }
}
