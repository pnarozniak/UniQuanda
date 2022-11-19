using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Report.CreateReport;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Tests.CQRS.Commands.Report
{
    [TestFixture]
    public class CreateReportHandlerTests
    {
        [SetUp]
        public void SetupTests()
        {
            this.reportRepository = new Mock<IReportRepository>();
            this.createReportHandler = new CreateReportHandler(this.reportRepository.Object);
            this.createReportCommand = new CreateReportCommand(new()
            {
                ReportedEntityId = 1,
                ReportTypeId = 2,
                Description = "",
            }, 1);
        }

        private CreateReportCommand createReportCommand;
        private CreateReportHandler createReportHandler;
        private Mock<IReportRepository> reportRepository;

        [Test]
        public async Task CreateReport_ShouldReturnTrue_WhenReportIsCreated()
        {
            reportRepository
                .Setup(rr => rr.CreateReportAsync(It.IsAny<CreateReportCommand>(), CancellationToken.None))
                .ReturnsAsync(true);

            var result = await createReportHandler.Handle(createReportCommand, CancellationToken.None);
            result.Should().BeTrue();
        }

        [Test]
        public async Task CreateReport_ShouldReturnFalse_WhenReportIsNotCreated()
        {
            reportRepository
                .Setup(rr => rr.CreateReportAsync(It.IsAny<CreateReportCommand>(), CancellationToken.None))
                .ReturnsAsync(false);

            var result = await createReportHandler.Handle(createReportCommand, CancellationToken.None);
            result.Should().BeFalse();
        }
    }
}