using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.CancelEmailConfirmation;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Tests.CQRS.Commands.Auth.CancelEmailConfirmation;

[TestFixture]
public class CancelEmailConfirmationHandlerTests
{
    private const int IdUser = 1;

    private CancelEmailConfirmationHandler cancelEmailConfirmationHandler;
    private CancelEmailConfirmationCommand cancelEmailConfirmationCommand;
    private Mock<IAuthRepository> authRepository;

    [SetUp]
    public void SetupTests()
    {
        this.authRepository = new Mock<IAuthRepository>();

        this.cancelEmailConfirmationCommand = new CancelEmailConfirmationCommand(IdUser);

        this.cancelEmailConfirmationHandler = new CancelEmailConfirmationHandler(this.authRepository.Object);
    }

    [Test]
    public async Task CancelEmailConfirmation_ShouldReturnTrue_WhenConfirmationIsSuccessful()
    {
        this.authRepository
            .Setup(au => au.CancelEmailConfirmationActionAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(true);

        var result = await cancelEmailConfirmationHandler.Handle(this.cancelEmailConfirmationCommand, CancellationToken.None);

        result.Should().Be(true);
    }

    [Test]
    public async Task CancelEmailConfirmation_ShouldReturnFalse_WhenConfirmationIsUnSuccessful()
    {
        this.authRepository
            .Setup(au => au.CancelEmailConfirmationActionAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(false);

        var result = await cancelEmailConfirmationHandler.Handle(this.cancelEmailConfirmationCommand, CancellationToken.None);

        result.Should().Be(false);
    }
}