using UniQuanda.Core.Application.CQRS.Commands.Report.CreateReport;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.Repositories
{
    public interface IReportRepository
    {
        /// <summary>
        ///     Gets report types from given category
        /// </summary>
        /// <param name="reportCategory">Report category</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Collection of report types</returns>
        public Task<IEnumerable<ReportTypeEntity>> GetReportTypesAsync(ReportCategoryEnum reportCategory, CancellationToken ct);

        /// <summary>
        ///     Creates new report
        /// </summary>
        /// <param name="reportData">Report data</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>True if report was created, otherwise False</returns>
        public Task<bool> CreateReportAsync(CreateReportCommand reportData, CancellationToken ct);
    }
}