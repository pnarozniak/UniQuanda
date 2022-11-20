using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateMainEmail;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Tests.CQRS.Commands.Auth.UpdateMainEmail;

[TestFixture]
public class UpdateMainEmailHandlerTests
{
    private const int IdUser = 1;
    private const string PlainPassword = "PlainPassword";
    private const string HashedPassword = "HashedPassword";
    private const string Nickname = "Nickname";
    private const string MainUserEmail = "mainEmail@domain.com";
    private const string ExtraUserEmail = "extraEmail@domain.com";
    private const int IdMainEmail = 1;
    private const int IdExtraEmail = 2;
    private const string NewMainEmail = "newMainEmail@domain.com";
    private const int EmailConfirmationExpirationInHours = 24;
    private readonly string _emailConfirmationToken = Guid.NewGuid().ToString();

    private UpdateMainEmailHandler updateMainEmailHandler;
    private UpdateMainEmailCommand updateMainEmailCommand;
    private Mock<IAuthRepository> authRepository;
    private Mock<IPasswordsService> passwordsService;
    private Mock<IEmailService> emailService;
    private Mock<ITokensService> tokensService;
    private Mock<IExpirationService> expirationService;

    [SetUp]
    public void SetupTests()
    {
        this.authRepository = new Mock<IAuthRepository>();
        this.passwordsService = new Mock<IPasswordsService>();
        this.emailService = new Mock<IEmailService>();
        this.tokensService = new Mock<ITokensService>();
        this.expirationService = new Mock<IExpirationService>();

        this.SetupEmailConfirmationToken();
        this.SetupExpirationService();

        this.updateMainEmailHandler = new UpdateMainEmailHandler(this.authRepository.Object, this.passwordsService.Object, this.emailService.Object, this.tokensService.Object, this.expirationService.Object);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultEnumSuccessful_WhenCallIsValidAndIdExtraEmailIsGiven()
    {
        this.SetupUpdateMainEmailCommand(IdExtraEmail, null);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.UpdateUserMainEmailByExtraEmailAsync(It.IsAny<UserEmailToConfirm>(), CancellationToken.None))
            .ReturnsAsync(true);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.Successful);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultEnumContentNotExist_WhenExtraEmailNotExistsdAndIdExtraEmailIsGiven()
    {
        this.SetupUpdateMainEmailCommand(2, null);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.UpdateUserMainEmailByExtraEmailAsync(It.IsAny<UserEmailToConfirm>(), CancellationToken.None))
            .ReturnsAsync((bool?)null);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.ContentNotExist);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultEnumDbConflict_WhenUpdateIsNotSuccessfuldAndIdExtraEmailIsGiven()
    {
        this.SetupUpdateMainEmailCommand(3, null);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.UpdateUserMainEmailByExtraEmailAsync(It.IsAny<UserEmailToConfirm>(), CancellationToken.None))
            .ReturnsAsync(false);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.DbConflict);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultEnumEmailNotAvailable_WhenGivenEmailIsNotAvailableAndIdExtraEmailIsNotGiven()
    {
        this.SetupUpdateMainEmailCommand(null, NewMainEmail);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.IsEmailAvailableAsync(this.updateMainEmailCommand.IdUser, this.updateMainEmailCommand.NewMainEmail, CancellationToken.None))
            .ReturnsAsync(false);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.EmailNotAvailable);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultEnumContentNotExist_WhenEmailIsConnectedWithUserAsExtraEmailAndMainEmailNotExistslAndIdExtraEmailIsNotGiven()
    {
        this.SetupUpdateMainEmailCommand(null, NewMainEmail);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowWhenNewMailEmailIsAvailableAndConnectedWithUserAsExtraEmail(null);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.ContentNotExist);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultEnumDbConflict_WhenEmailIsConnectedWithUserAsExtraEmailAndUpdateEmailIsNotSuccessfulAndIdExtraEmailIsNotGiven()
    {
        this.SetupUpdateMainEmailCommand(null, NewMainEmail);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowWhenNewMailEmailIsAvailableAndConnectedWithUserAsExtraEmail(false);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.DbConflict);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultEnumSuccessful_WhenEmailIsConnectedWithUserAsExtraEmailAndUpdateEmailIsSuccessfulAndIdExtraEmailIsNotGiven()
    {
        this.SetupUpdateMainEmailCommand(null, NewMainEmail);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowWhenNewMailEmailIsAvailableAndConnectedWithUserAsExtraEmail(true);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.Successful);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultSuccessfult_WhenGivenEmailIsConnectedWithUserAsMainEmailAndIdExtraEmailIsNotGiven()
    {
        this.SetupUpdateMainEmailCommand(null, NewMainEmail);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.IsEmailAvailableAsync(this.updateMainEmailCommand.IdUser, this.updateMainEmailCommand.NewMainEmail, CancellationToken.None))
            .ReturnsAsync(true);
        this.authRepository
            .Setup(ar => ar.GetExtraEmailIdAsync(this.updateMainEmailCommand.IdUser, this.updateMainEmailCommand.NewMainEmail, CancellationToken.None))
            .ReturnsAsync((isConnected: true, idEmail: null));

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.Successful);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultContentNotExist_WhenEmailIsNotConnectedAndMainEmailNotExistAndIdExtraEmailIsNotGiven()
    {
        this.SetupUpdateMainEmailCommand(null, NewMainEmail);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowWhenGivenEmailIsNotConnectedWithUser(null);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.ContentNotExist);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultDbConflict_WhenEmailIsNotConnectedAndUpdateIsNotSuccessfulAndIdExtraEmailIsNotGiven()
    {
        this.SetupUpdateMainEmailCommand(null, NewMainEmail);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowWhenGivenEmailIsNotConnectedWithUser(false);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.DbConflict);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultSuccessful_WhenEmailIsNotConnectedAndCallIsValidAndIdExtraEmailIsNotGiven()
    {
        this.SetupUpdateMainEmailCommand(null, NewMainEmail);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowWhenGivenEmailIsNotConnectedWithUser(true);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.Successful);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultEnumContentNotExist_WhenUserNotExists()
    {
        this.SetupUpdateMainEmailCommand();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync((UserSecurityEntity)null);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.ContentNotExist);
    }

    [Test]
    public async Task UpdateMainEmail_ShouldReturnResultEnumInvalidPassword_WhenGivenPasswordIsInvalid()
    {
        this.SetupUpdateMainEmailCommand(1, null, "InvalidPassword");
        var userSecurityEntity = GetUserSecurityEntity();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);
        this.passwordsService
            .Setup(ps => ps.VerifyPassword(this.updateMainEmailCommand.PlainPassword, userSecurityEntity.HashedPassword))
            .Returns(false);

        var result = await updateMainEmailHandler.Handle(this.updateMainEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.InvalidPassword);
    }

    private void SetupUpdateMainEmailCommand(int? idExtraEmail = IdExtraEmail, string newMainEmail = NewMainEmail, string plainPassword = PlainPassword)
    {
        var updateMainEmailRequestDTO = new UpdateMainEmailRequestDTO
        {
            IdExtraEmail = idExtraEmail,
            NewMainEmail = newMainEmail,
            Password = plainPassword
        };
        this.updateMainEmailCommand = new(updateMainEmailRequestDTO, IdUser, new UserAgentInfo{});
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
            .Setup(es => es.GetNewUserExpirationInHours())
            .Returns(EmailConfirmationExpirationInHours);
    }

    private void SetupValidPasswordFlow(UserSecurityEntity userSecurityEntity)
    {
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);
        this.passwordsService
            .Setup(ps => ps.VerifyPassword(this.updateMainEmailCommand.PlainPassword, userSecurityEntity.HashedPassword))
            .Returns(true);
    }

    private void SetupFlowWhenNewMailEmailIsAvailableAndConnectedWithUserAsExtraEmail(bool? updateResult)
    {
        this.authRepository
            .Setup(ar => ar.IsEmailAvailableAsync(this.updateMainEmailCommand.IdUser, this.updateMainEmailCommand.NewMainEmail, CancellationToken.None))
            .ReturnsAsync(true);
        this.authRepository
            .Setup(ar => ar.GetExtraEmailIdAsync(this.updateMainEmailCommand.IdUser, this.updateMainEmailCommand.NewMainEmail, CancellationToken.None))
            .ReturnsAsync((isConnected: true, idEmail: IdExtraEmail));
        this.authRepository
            .Setup(ar => ar.UpdateUserMainEmailByExtraEmailAsync(It.IsAny<UserEmailToConfirm>(), CancellationToken.None))
            .ReturnsAsync(updateResult);
    }

    private void SetupFlowWhenGivenEmailIsNotConnectedWithUser(bool? updateResult)
    {
        this.authRepository
            .Setup(ar => ar.IsEmailAvailableAsync(this.updateMainEmailCommand.IdUser, this.updateMainEmailCommand.NewMainEmail, CancellationToken.None))
            .ReturnsAsync(true);
        this.authRepository
            .Setup(ar => ar.GetExtraEmailIdAsync(this.updateMainEmailCommand.IdUser, this.updateMainEmailCommand.NewMainEmail, CancellationToken.None))
            .ReturnsAsync((isConnected: false, idEmail: null));
        this.authRepository
            .Setup(ar => ar.AddUserMainEmailToConfirmAsync(It.IsAny<UserEmailToConfirm>(), CancellationToken.None))
            .ReturnsAsync(updateResult);
    }
}