using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.UpdatePassword;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Tests.CQRS.Commands.Auth.UpdatePassword;

[TestFixture]
public class UpdatePasswordHandlerTests
{
    private const int IdUser = 1;
    private const string NewPlainPassword = "NewPlainPassword";
    private const string OldPlainPassword = "OldPlainPassword";
    private const string NewHashedPassword = "NewHashedPassword";
    private const string OldHashedPassword = "OldHashedPassword";
    private const string Nickname = "Nickname";
    private const string MainUserEmail = "mainEmail@domain.com";
    private const string ExtraUserEmail = "extraEmail@domain.com";

    private UpdatePasswordHandler updatePasswordHandler;
    private UpdatePasswordCommand updatePasswordCommand;
    private Mock<IAuthRepository> authRepository;
    private Mock<IPasswordsService> passwordsService;
    private Mock<IEmailService> emailService;

    [SetUp]
    public void SetupTests()
    {
        this.authRepository = new Mock<IAuthRepository>();
        this.passwordsService = new Mock<IPasswordsService>();
        this.emailService = new Mock<IEmailService>();

        this.updatePasswordHandler = new UpdatePasswordHandler(this.authRepository.Object, this.passwordsService.Object, this.emailService.Object);
    }

    [Test]
    public async Task UpdatePassword_ShouldReturnResultEnumSuccessful_WhenCallIsValid()
    {
        this.SetupUpdatePasswordCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.UpdateUserPasswordAsync(IdUser, NewHashedPassword, CancellationToken.None))
            .ReturnsAsync(true);

        var result = await updatePasswordHandler.Handle(this.updatePasswordCommand, CancellationToken.None);

        result.ActionResult.Should().Be(AppUserSecurityActionResultEnum.Successful);
    }

    [Test]
    public async Task UpdatePassword_ShouldReturnResultEnumContentNotExist_WhenUserNotExistsDuringUpdate()
    {
        this.SetupUpdatePasswordCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.UpdateUserPasswordAsync(IdUser, NewHashedPassword, CancellationToken.None))
            .ReturnsAsync((bool?)null);

        var result = await updatePasswordHandler.Handle(this.updatePasswordCommand, CancellationToken.None);

        result.ActionResult.Should().Be(AppUserSecurityActionResultEnum.ContentNotExist);
    }

    [Test]
    public async Task UpdatePassword_ShouldReturnResultEnumDbConflict_WhenUpdateIsNotSuccessful()
    {
        this.SetupUpdatePasswordCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.UpdateUserPasswordAsync(IdUser, NewHashedPassword, CancellationToken.None))
            .ReturnsAsync(false);

        var result = await updatePasswordHandler.Handle(this.updatePasswordCommand, CancellationToken.None);

        result.ActionResult.Should().Be(AppUserSecurityActionResultEnum.UnSuccessful);
    }

    [Test]
    public async Task UpdatePassword_ShouldReturnResultEnumContentNotExist_WhenUserNotExists()
    {
        this.SetupUpdatePasswordCommand();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync((UserSecurityEntity)null);

        var result = await updatePasswordHandler.Handle(this.updatePasswordCommand, CancellationToken.None);

        result.ActionResult.Should().Be(AppUserSecurityActionResultEnum.ContentNotExist);
    }

    [Test]
    public async Task UpdatePassword_ShouldReturnResultEnumInvalidPassword_WhenGivenPasswordIsInvalid()
    {
        this.SetupUpdatePasswordCommand("InvalidPassword");
        var userSecurityEntity = GetUserSecurityEntity();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);
        this.passwordsService
            .Setup(ps => ps.VerifyPassword(this.updatePasswordCommand.OldPassword, userSecurityEntity.HashedPassword))
            .Returns(false);

        var result = await updatePasswordHandler.Handle(this.updatePasswordCommand, CancellationToken.None);

        result.ActionResult.Should().Be(AppUserSecurityActionResultEnum.InvalidPassword);
    }

    private void SetupUpdatePasswordCommand(string oldPlainPassword = OldPlainPassword)
    {
        var updatePasswordRequestDTO = new UpdatePasswordRequestDTO
        {
            NewPassword = NewPlainPassword,
            OldPassword = oldPlainPassword
        };
        this.updatePasswordCommand = new(updatePasswordRequestDTO, IdUser, new UserAgentInfo { });
    }

    private static UserSecurityEntity GetUserSecurityEntity()
    {
        return new UserSecurityEntity
        {
            Nickname = Nickname,
            HashedPassword = OldHashedPassword,
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
            .Setup(ps => ps.VerifyPassword(this.updatePasswordCommand.OldPassword, userSecurityEntity.HashedPassword))
            .Returns(true);
        this.passwordsService
            .Setup(ps => ps.HashPassword(this.updatePasswordCommand.NewPassword))
            .Returns(NewHashedPassword);
    }

}
