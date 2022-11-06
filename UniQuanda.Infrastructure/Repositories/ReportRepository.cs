using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.CQRS.Commands.Report.CreateReport;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

public class ReportRepository : IReportRepository
{
	private readonly AppDbContext _appContext;
	public ReportRepository(AppDbContext appContext)
	{
		_appContext = appContext;
	}

	public async Task<IEnumerable<ReportTypeEntity>> GetReportTypesAsync(ReportCategoryEnum reportCategory, CancellationToken ct)
	{
		return await this._appContext.ReportTypes
			.Where(rt => rt.ReportCategory == reportCategory)
			.Select(rt => new ReportTypeEntity{
				Id = rt.Id,
				Name = rt.Name
			}).ToListAsync(ct);
	}

	public async Task<bool> CreateReportAsync(CreateReportCommand reportData, CancellationToken ct)
	{
		var reportType = await this._appContext.ReportTypes
			.Where(rt => rt.Id == reportData.ReportedTypeId)
			.SingleOrDefaultAsync(ct);

		if (reportType is null) return false;
	
		var report = new Report
		{
			ReporterId = reportData.ReporterId,
			Description = reportData.Description,
			ReportTypeId = reportData.ReportedTypeId,
			ReportedQuestionId = reportType.ReportCategory == ReportCategoryEnum.QUESTION ? reportData.ReportedEntityId : null,
			ReportedAnswerId = reportType.ReportCategory == ReportCategoryEnum.ANSWER ? reportData.ReportedEntityId : null,
			ReportedUserId = reportType.ReportCategory == ReportCategoryEnum.USER ? reportData.ReportedEntityId : null,
			CreatedAt = DateTime.UtcNow
		};

		await this._appContext.Reports.AddAsync(report, ct);
		return await _appContext.SaveChangesAsync(ct) == 1;
	}
}