using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Queries.Auth.GetReportTypes;
using UniQuanda.Core.Application.CQRS.Queries.Report.GetReportTypes;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Tests.CQRS.Queries.Report
{
    [TestFixture]
    public class GetReportTypesHandlerTests
    {
        [SetUp]
        public void SetupTests()
        {
            this.reportRepository = new Mock<IReportRepository>();
            this.getReportTypesHandler = new GetReportTypesHandler(this.reportRepository.Object);
        }

        private GetReportTypesQuery getReportTypesQuery;
        private GetReportTypesHandler getReportTypesHandler;
        private Mock<IReportRepository> reportRepository;

        [Test]
        public async Task GetReportTypes_ShouldReturnNull_WhenReportCategoryIsInvalidOrNotSpecified()
        {
            var query = new GetReportTypesQuery(new GetReportTypesRequestDTO()) { };
            var result = await getReportTypesHandler.Handle(query, CancellationToken.None);
            result.Should().BeNull();
        }


        [Test]
        public async Task GetReportTypes_ShouldReturnReportTypes_WhenReportCategoryIsValid()
        {
            var foundReportTypes = new List<ReportTypeEntity>() { new ReportTypeEntity { Id = 1, Name = "Name" } };

            reportRepository
                .Setup(rr => rr.GetReportTypesAsync(It.IsAny<ReportCategoryEnum>(), CancellationToken.None))
                .ReturnsAsync(foundReportTypes);

            var query = new GetReportTypesQuery(new GetReportTypesRequestDTO()
            {
                Answer = true
            })
            { };
            var result = await getReportTypesHandler.Handle(query, CancellationToken.None);
            var shouldReturn = new GetReportTypesResponseDTO()
            {
                Items = new List<ReportTypeResponseDTO>() { new ReportTypeResponseDTO { Id = 1, Name = "Name" } }
            };
            result.Should().BeEquivalentTo(shouldReturn);
        }
    }
}