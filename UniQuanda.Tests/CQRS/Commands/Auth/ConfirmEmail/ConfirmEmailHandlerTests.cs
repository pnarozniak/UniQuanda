using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmEmail;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Tests.CQRS.Commands.Auth.ConfirmEmail;

[TestFixture]
public class ConfirmEmailHandlerTests
{
    private const string Email = "email@uniquanda.pl";
    private const string ConfirmationCode = "ConfirmationCode";
    private const string MainUserEmail = "mainEmail@domain.com";

    private ConfirmEmailHandler confirmEmailHandler;
    private ConfirmEmailCommand confirmEmailCommand;
    private Mock<IAuthRepository> authRepository;
    private Mock<IEmailService> emailService;

    [SetUp]
    public void SetupTests()
    {
        authRepository = new Mock<IAuthRepository>();
        emailService = new Mock<IEmailService>();

        this.SetupConfirmEmailCommand();

        this.confirmEmailHandler = new ConfirmEmailHandler(this.authRepository.Object, this.emailService.Object);
    }

    [Test]
    public async Task ConfirmEmail_ShouldReturnTrue_WhenConfirmationIsSuccessfulAndIsMainEmail()
    {
        this.authRepository
            .Setup(ar => ar.GetMainEmailByEmailToConfirmAsync(Email, CancellationToken.None))
            .ReturnsAsync(MainUserEmail);
        this.authRepository
            .Setup(ar => ar.ConfirmUserEmailAsync(Email, ConfirmationCode, CancellationToken.None))
            .ReturnsAsync((isSuccess: true, isMainEmail: true));

        var result = await confirmEmailHandler.Handle(this.confirmEmailCommand, CancellationToken.None);

        result.Should().Be(true);
    }

    [Test]
    public async Task ConfirmEmail_ShouldReturnTrue_WhenConfirmationIsSuccessfulAndIsExtraEmail()
    {
        this.authRepository
            .Setup(ar => ar.GetMainEmailByEmailToConfirmAsync(Email, CancellationToken.None))
            .ReturnsAsync(MainUserEmail);
        this.authRepository
            .Setup(ar => ar.ConfirmUserEmailAsync(Email, ConfirmationCode, CancellationToken.None))
            .ReturnsAsync((isSuccess: true, isMainEmail: false));

        var result = await confirmEmailHandler.Handle(this.confirmEmailCommand, CancellationToken.None);

        result.Should().Be(true);
    }

    [Test]
    public async Task ConfirmEmail_ShouldReturnFalse_WhenMainEmailNotExists()
    {
        this.authRepository
            .Setup(ar => ar.GetMainEmailByEmailToConfirmAsync(Email, CancellationToken.None))
            .ReturnsAsync(null as string);

        var result = await confirmEmailHandler.Handle(this.confirmEmailCommand, CancellationToken.None);

        result.Should().Be(false);
    }

    [Test]
    public async Task ConfirmEmail_ShouldReturnFalse_WhenConfirmationIsUnSuccessful()
    {
        this.authRepository
            .Setup(ar => ar.GetMainEmailByEmailToConfirmAsync(Email, CancellationToken.None))
            .ReturnsAsync(MainUserEmail);
        this.authRepository
            .Setup(ar => ar.ConfirmUserEmailAsync(Email, ConfirmationCode, CancellationToken.None))
            .ReturnsAsync((isSuccess: false, isMainEmail: false));

        var result = await confirmEmailHandler.Handle(this.confirmEmailCommand, CancellationToken.None);

        result.Should().Be(false);
    }

    private void SetupConfirmEmailCommand()
    {
        var request = new ConfirmEmailRequestDTO
        {
            Email = Email,
            ConfirmationCode = ConfirmationCode
        };
        this.confirmEmailCommand = new ConfirmEmailCommand(request, new UserAgentInfo { });
    }
}