using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmEmail;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Tests.CQRS.Commands.Auth.ConfirmEmail;

[TestFixture]
public class ConfirmEmailHandlerTests
{
    private const string Email = "email@uniquanda.pl";
    private const string ConfirmationCode = "ConfirmationCode";
    private const int IdUser = 1;
    private const string Nickname = "Nickname";
    private const string MainUserEmail = "mainEmail@domain.com";
    private const string ExtraUserEmail = "extraEmail@domain.com";
    private const int IdMainEmail = 1;
    private const int IdExtraEmail = 2;

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
            .Setup(ar => ar.ConfirmUserEmailAsync(Email, ConfirmationCode, CancellationToken.None))
            .ReturnsAsync((isSuccess: true, isMainEmail: true, idUser: IdUser));
        var userSecurityEntity = GetUserSecurityEntity();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);

        var result = await confirmEmailHandler.Handle(this.confirmEmailCommand, CancellationToken.None);

        result.Should().Be(true);
    }

    [Test]
    public async Task ConfirmEmail_ShouldReturnTrue_WhenConfirmationIsSuccessfulAndIsExtraEmail()
    {
        this.authRepository
            .Setup(ar => ar.ConfirmUserEmailAsync(Email, ConfirmationCode, CancellationToken.None))
            .ReturnsAsync((isSuccess: true, isMainEmail: false, idUser: IdUser));
        var userSecurityEntity = GetUserSecurityEntity();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);

        var result = await confirmEmailHandler.Handle(this.confirmEmailCommand, CancellationToken.None);

        result.Should().Be(true);
    }

    [Test]
    public async Task ConfirmEmail_ShouldReturnTrue_WhenConfirmationIsUnSuccessful()
    {
        this.authRepository
            .Setup(ar => ar.ConfirmUserEmailAsync(Email, ConfirmationCode, CancellationToken.None))
            .ReturnsAsync((isSuccess: false, isMainEmail: false, idUser: null));

        var result = await confirmEmailHandler.Handle(this.confirmEmailCommand, CancellationToken.None);

        result.Should().Be(false);
    }

    [Test]
    public async Task ConfirmEmail_ShouldReturnFalse_WhenUserNotExists()
    {
        this.authRepository
            .Setup(ar => ar.ConfirmUserEmailAsync(Email, ConfirmationCode, CancellationToken.None))
            .ReturnsAsync((isSuccess: true, isMainEmail: true, idUser: IdUser));
        var userSecurityEntity = GetUserSecurityEntity();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync((UserSecurityEntity?)null);

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
        this.confirmEmailCommand = new ConfirmEmailCommand(request, 1, new UserAgentInfo { });
    }

    private static UserSecurityEntity GetUserSecurityEntity()
    {
        return new UserSecurityEntity
        {
            Nickname = Nickname,
            Emails = new List<UserEmailSecurity>()
            {
                new UserEmailSecurity { Id = IdMainEmail, IsMain = true, Value = MainUserEmail},
                new UserEmailSecurity { Id = IdExtraEmail, IsMain = false, Value = ExtraUserEmail}
            }
        };
    }
}