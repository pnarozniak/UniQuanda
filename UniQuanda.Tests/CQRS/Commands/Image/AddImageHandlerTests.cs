using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Tests.CQRS.Commands.Auth.Login
{
    [TestFixture]
    public class AddImageHandlerTests
    {
        private Mock<IImageService> imageService;
        private AddImageHandler addImageHandler;
        private AddImageCommand addImageCommand;

        [SetUp]
        public void SetupTests()
        {
            this.imageService = new Mock<IImageService>();
            this.addImageHandler = new AddImageHandler(this.imageService.Object);
            this.addImageCommand = new AddImageCommand(new()
            {
                Image = new FormFile(Stream.Null, 0, 0, "name", "name"),
                ImageName = "Name"
            }, ImageFolder.Profile);
        }

        [Test]
        public async Task AddImage_ShouldBeSuccess_WhenImageIsAdded()
        {
            this.imageService.Setup(imgs => imgs.SaveImageAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<ImageFolder>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var result = await this.addImageHandler.Handle(this.addImageCommand, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public async Task AddImage_ShouldBeFail_WhenImageIsNotAdded()
        {
            this.imageService.Setup(imgs => imgs.SaveImageAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<ImageFolder>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var result = await this.addImageHandler.Handle(this.addImageCommand, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }
    }
}