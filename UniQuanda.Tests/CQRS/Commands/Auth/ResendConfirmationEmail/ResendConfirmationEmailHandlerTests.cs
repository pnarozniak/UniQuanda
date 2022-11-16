using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ResendConfirmationEmail;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Tests.CQRS.Commands.Auth.ResendConfirmationEmail;

[TestFixture]
public class ResendConfirmationEmailHandlerTests
{
    private const int IdUser = 1;
    private const string PlainPassword = "PlainPassword";
    private const string HashedPassword = "HashedPassword";
    private const string Nickname = "Nickname";
    private const string MainUserEmail = "mainEmail@domain.com";
    private const string ExtraUserEmail = "extraEmail@domain.com";
    private const int IdMainEmail = 1;
    private const int IdExtraEmail = 2;
    private const int EmailConfirmationExpirationInHours = 24;
    private readonly string _emailConfirmationToken = Guid.NewGuid().ToString();

    private ResendConfirmationEmailHandler resendConfirmationEmailHandler;
    private ResendConfirmationEmailCommand resendConfirmationEmailCommand;
    private Mock<IAuthRepository> authRepository;
    private Mock<IEmailService> emailService;
    private Mock<ITokensService> tokensService;
    private Mock<IExpirationService> expirationService;

    [SetUp]
    public void SetupTests()
    {
        this.authRepository = new Mock<IAuthRepository>();
        this.emailService = new Mock<IEmailService>();
        this.tokensService = new Mock<ITokensService>();
        this.expirationService = new Mock<IExpirationService>();

        this.SetupEmailConfirmationToken();
        this.SetupExpirationService();
        this.resendConfirmationEmailCommand = new(IdUser);

        this.resendConfirmationEmailHandler = new ResendConfirmationEmailHandler(this.authRepository.Object, this.emailService.Object, this.tokensService.Object, this.expirationService.Object);
    }

    [Test]
    public async Task ResendConfirmationEmail_ShouldReturnTrue_WhenUpdateIsSuccessful()
    {
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidFlowWhenUserAndEmailExists(userSecurityEntity, IdExtraEmail);
        this.authRepository
            .Setup(ar => ar.UpdateActionToConfirmEmailAsync(It.IsAny<UserEmailToConfirm>(), CancellationToken.None))
            .ReturnsAsync(true);

        var result = await resendConfirmationEmailHandler.Handle(this.resendConfirmationEmailCommand, CancellationToken.None);

        result.Should().Be(true);
    }

    [Test]
    public async Task ResendConfirmationEmail_ShouldReturnFalse_WhenUpdateIsUnSuccessful()
    {
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidFlowWhenUserAndEmailExists(userSecurityEntity, IdExtraEmail);
        this.authRepository
            .Setup(ar => ar.UpdateActionToConfirmEmailAsync(It.IsAny<UserEmailToConfirm>(), CancellationToken.None))
            .ReturnsAsync(false);

        var result = await resendConfirmationEmailHandler.Handle(this.resendConfirmationEmailCommand, CancellationToken.None);

        result.Should().Be(false);
    }

    [Test]
    public async Task ResendConfirmationEmail_ShouldReturnFalse_WhenUserNotExists()
    {

        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync((UserSecurityEntity)null);

        var result = await resendConfirmationEmailHandler.Handle(this.resendConfirmationEmailCommand, CancellationToken.None);

        result.Should().Be(false);
    }

    [Test]
    public async Task ResendConfirmationEmail_ShouldReturnFalse_WhenNotExistsEmailToConfirm()
    {
        var userSecurityEntity = GetUserSecurityEntity();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.GetIdEmailToConfirmAsync(IdUser, CancellationToken.None))
            .ReturnsAsync((int?)null);

        var result = await resendConfirmationEmailHandler.Handle(this.resendConfirmationEmailCommand, CancellationToken.None);

        result.Should().Be(false);
    }

    private static UserSecurityEntity GetUserSecurityEntity()
    {
        return new UserSecurityEntity
        {
            Nickname = Nickname,
            HashedPassword = HashedPassword,
            Emails = new List<UserEmailSecurity>()
            {
                new UserEmailSecurity { Id = IdMainEmail, IsMain = true, Value = MainUserEmail},
                new UserEmailSecurity { Id = IdExtraEmail, IsMain = false, Value = ExtraUserEmail}
            }
        };
    }

    private void SetupEmailConfirmationToken()
    {
        this.tokensService
            .Setup(ts => ts.GenerateNewEmailConfirmationToken())
            .Returns(_emailConfirmationToken);
    }

    private void SetupExpirationService()
    {
        this.expirationService
            .Setup(es => es.GetEmailConfirmationExpirationInHours())
            .Returns(EmailConfirmationExpirationInHours);
    }

    private void SetupValidFlowWhenUserAndEmailExists(UserSecurityEntity userSecurityEntity, int? idEmail)
    {
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.GetIdEmailToConfirmAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(idEmail);
    }
}