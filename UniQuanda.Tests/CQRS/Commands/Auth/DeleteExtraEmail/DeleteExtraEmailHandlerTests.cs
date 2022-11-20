using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.DeleteExtraEmail;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Tests.CQRS.Commands.Auth.DeleteExtraEmail;

[TestFixture]
public class DeleteExtraEmailHandlerTests
{
    private const int IdUser = 1;
    private const string PlainPassword = "PlainPassword";
    private const string HashedPassword = "HashedPassword";
    private const string Nickname = "Nickname";
    private const string MainUserEmail = "mainEmail@domain.com";
    private const string ExtraUserEmail = "extraEmail@domain.com";
    private const int IdExtraEmail = 2;

    private DeleteExtraEmailHandler deleteExtraEmailHandler;
    private DeleteExtraEmailCommand deleteExtraEmailCommand;
    private Mock<IAuthRepository> authRepository;
    private Mock<IPasswordsService> passwordsService;
    private Mock<IEmailService> emailService;

    [SetUp]
    public void SetupTests()
    {
        this.authRepository = new Mock<IAuthRepository>();
        this.passwordsService = new Mock<IPasswordsService>();
        this.emailService = new Mock<IEmailService>();

        this.deleteExtraEmailHandler = new DeleteExtraEmailHandler(this.authRepository.Object, this.passwordsService.Object, this.emailService.Object);
    }

    [Test]
    public async Task DeleteExtraEmail_ShouldReturnResultEnumSuccessful_WhenCallIsValid()
    {
        this.SetupDeleteExtraEmailCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.DeleteExtraEmailAsync(IdUser, IdExtraEmail, CancellationToken.None))
            .ReturnsAsync(true);

        var result = await deleteExtraEmailHandler.Handle(this.deleteExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.Successful);
    }

    [Test]
    public async Task DeleteExtraEmail_ShouldReturnResultEnumContentNotExist_WhenUserNotExists()
    {
        this.SetupDeleteExtraEmailCommand();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync((UserSecurityEntity)null);

        var result = await deleteExtraEmailHandler.Handle(this.deleteExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.ContentNotExist);
    }

    [Test]
    public async Task DeleteExtraEmail_ShouldReturnResultEnumInvalidPassword_WhenGivenPasswordIsInvalid()
    {
        this.SetupDeleteExtraEmailCommand("InvalidPassword");
        var userSecurityEntity = GetUserSecurityEntity();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);
        this.passwordsService
            .Setup(ps => ps.VerifyPassword(this.deleteExtraEmailCommand.Password, userSecurityEntity.HashedPassword))
            .Returns(false);

        var result = await deleteExtraEmailHandler.Handle(this.deleteExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.InvalidPassword);
    }

    [Test]
    public async Task DeleteExtraEmail_ShouldReturnResultEnumNotExist_WhenGivenEmailIsMain()
    {
        this.SetupDeleteExtraEmailCommand(PlainPassword, 1);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.DeleteExtraEmailAsync(IdUser, IdExtraEmail, CancellationToken.None))
            .ReturnsAsync((bool?)null);

        var result = await deleteExtraEmailHandler.Handle(this.deleteExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.ContentNotExist);
    }

    [Test]
    public async Task DeleteExtraEmail_ShouldReturnResultEnumDbConflict_WhenDeleteExtraEmailIsNotSuccessful()
    {
        this.SetupDeleteExtraEmailCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.DeleteExtraEmailAsync(IdUser, IdExtraEmail, CancellationToken.None))
            .ReturnsAsync(false);

        var result = await deleteExtraEmailHandler.Handle(this.deleteExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.DbConflict);
    }

    private void SetupDeleteExtraEmailCommand(string plainPassword = PlainPassword, int idExtraEmail = IdExtraEmail)
    {
        var deleteExtraEmailRequestDTO = new DeleteExtraEmailRequestDTO
        {
            IdExtraEmail = IdExtraEmail,
            Password = plainPassword
        };
        this.deleteExtraEmailCommand = new(deleteExtraEmailRequestDTO, IdUser, new UserAgentInfo { });
    }

    private static UserSecurityEntity GetUserSecurityEntity()
    {
        return new UserSecurityEntity
        {
            Nickname = Nickname,
            HashedPassword = HashedPassword,
            Emails = new List<UserEmailSecurity>()
            {
                new UserEmailSecurity { Id = 1, IsMain = true, Value = MainUserEmail},
                new UserEmailSecurity { Id = 2, IsMain = false, Value = ExtraUserEmail}
            }
        };
    }

    private void SetupValidPasswordFlow(UserSecurityEntity userSecurityEntity)
    {
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);
        this.passwordsService
            .Setup(ps => ps.VerifyPassword(this.deleteExtraEmailCommand.Password, userSecurityEntity.HashedPassword))
            .Returns(true);
    }
}
