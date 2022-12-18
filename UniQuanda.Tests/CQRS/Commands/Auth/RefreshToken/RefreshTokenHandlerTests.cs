using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister;
using UniQuanda.Core.Application.CQRS.Commands.Auth.RefreshToken;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;

namespace UniQuanda.Tests.CQRS.Commands.Auth.RecoverPassword;

[TestFixture]
public class RefreshTokenHandlerTests
{
    [SetUp]
    public void SetupTests()
    {
        authRepository = new Mock<IAuthRepository>();
        tokensService = new Mock<ITokensService>();

        refreshTokenCommand = new RefreshTokenCommand(new RefreshTokenRequestDTO
        { AccessToken = AccessToken, RefreshToken = RefreshToken });
        refreshTokenHandler = new RefreshTokenHandler(authRepository.Object, tokensService.Object);
    }

    private const string RefreshToken = "RefreshToken";
    private const string AccessToken = "RefreshToken";

    private RefreshTokenCommand refreshTokenCommand;
    private RefreshTokenHandler refreshTokenHandler;
    private Mock<IAuthRepository> authRepository;
    private Mock<ITokensService> tokensService;

    [Test]
    public async Task RefreshToken_ShouldReturnNull_WhenAccessTokenIsInvalid()
    {
        int? idUserFromAccessToken = null;
        tokensService
            .Setup(ts => ts.GetUserIdFromExpiredAccessToken(It.IsAny<string>()))
            .Returns(idUserFromAccessToken);

        var result = await refreshTokenHandler.Handle(refreshTokenCommand, CancellationToken.None);
        result.Should().BeNull();
    }

    [Test]
    public async Task RefreshToken_ShouldReturnNull_WhenAccessTokenIsValidButRefreshTokenIsInvalid()
    {
        const int idUserFromAccessToken = 1;
        var userEntity = new UserEntity
        {
            Id = idUserFromAccessToken,
            RefreshToken = ""
        };
        tokensService
            .Setup(ts => ts.GetUserIdFromExpiredAccessToken(It.IsAny<string>()))
            .Returns(idUserFromAccessToken);

        authRepository
            .Setup(ar => ar.GetUserByIdAsync(idUserFromAccessToken, CancellationToken.None))
            .ReturnsAsync(userEntity);

        var result = await refreshTokenHandler.Handle(refreshTokenCommand, CancellationToken.None);
        result.Should().BeNull();
    }

    [Test]
    public async Task RefreshToken_ShouldReturnNull_WhenAccessAndRefreshTokensAreValidButRefreshTokenHasExpired()
    {
        const int idUserFromAccessToken = 1;
        var userEntity = new UserEntity
        {
            Id = idUserFromAccessToken,
            RefreshToken = RefreshToken,
            RefreshTokenExp = DateTime.UtcNow.AddHours(-2)
        };

        tokensService
            .Setup(ts => ts.GetUserIdFromExpiredAccessToken(It.IsAny<string>()))
            .Returns(idUserFromAccessToken);

        authRepository
            .Setup(ar => ar.GetUserByIdAsync(idUserFromAccessToken, CancellationToken.None))
            .ReturnsAsync(userEntity);

        var result = await refreshTokenHandler.Handle(refreshTokenCommand, CancellationToken.None);
        result.Should().BeNull();
    }

    [Test]
    public async Task RefreshToken_ShouldReturnNewTokens_WhenAccessAndRefreshTokensAreValid()
    {
        const string newRefreshToken = "newRefreshToken";
        var newRefreshTokenExp = DateTime.UtcNow.AddHours(2);
        const string newAccessToken = "newAccessToken";

        const int idUserFromAccessToken = 1;
        var userEntity = new UserEntity
        {
            Id = idUserFromAccessToken,
            RefreshToken = RefreshToken,
            RefreshTokenExp = DateTime.UtcNow.AddHours(2)
        };

        tokensService
            .Setup(ts => ts.GetUserIdFromExpiredAccessToken(It.IsAny<string>()))
            .Returns(idUserFromAccessToken);

        tokensService
            .Setup(ts => ts.GenerateRefreshToken())
            .Returns(new Tuple<string, DateTime>(newRefreshToken, newRefreshTokenExp));

        tokensService
            .Setup(ts => ts.GenerateAccessToken(userEntity.Id, It.IsAny<DateTime?>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .Returns(newAccessToken);

        authRepository
            .Setup(ar => ar.GetUserByIdAsync(idUserFromAccessToken, CancellationToken.None))
            .ReturnsAsync(userEntity);

        var result = await refreshTokenHandler.Handle(refreshTokenCommand, CancellationToken.None);
        result.Should().BeEquivalentTo(new RefreshTokenResponseDTO
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }
}