using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Tests.CQRS.Commands.Auth.Login
{
    [TestFixture]
    public class RemoveImageHandlerTests
    {
        private Mock<IImageService> imageService;
        private RemoveImageHandler removeImageHandler;
        private RemoveImageCommand removeImageCommand;

        [SetUp]
        public void SetupTests()
        {
            this.imageService = new Mock<IImageService>();
            this.removeImageHandler = new RemoveImageHandler(this.imageService.Object);
            this.removeImageCommand = new RemoveImageCommand(new()
            {
                FileName = "Name"
            }, ImageFolder.Profile);
        }

        [Test]
        public async Task RemoveImage_ShouldBeSuccess_WhenImageIsRemoved()
        {
            this.imageService.Setup(imgs => imgs.RemoveImageAsync(It.IsAny<string>(), It.IsAny<ImageFolder>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var result = await this.removeImageHandler.Handle(this.removeImageCommand, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public async Task AddImage_ShouldBeFail_WhenImageIsNotAdded()
        {
            this.imageService.Setup(imgs => imgs.RemoveImageAsync(It.IsAny<string>(), It.IsAny<ImageFolder>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var result = await this.removeImageHandler.Handle(this.removeImageCommand, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }
    }
}