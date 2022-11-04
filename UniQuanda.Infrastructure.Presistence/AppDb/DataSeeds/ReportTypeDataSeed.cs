using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;
using static UniQuanda.Core.Domain.Enums.ReportCategoryEnum;

public class ReportTypeDataSeed : IEntityTypeConfiguration<ReportType>
	{
		public ReportTypeDataSeed()
		{
		}

		public void Configure(EntityTypeBuilder<ReportType> builder)
		{
				builder.HasData(
					new ReportType{ Id = 1, Name = "Podszywanie się pod inną osobę", ReportCategory = USER },
					new ReportType{ Id = 2, Name = "Publikowanie niestosownych treści", ReportCategory = USER },
					new ReportType{ Id = 3, Name = "Nękanie lub cybeprzemoc", ReportCategory = USER },
					new ReportType{ Id = 4, Name = "Inne", ReportCategory = USER },
					new ReportType{ Id = 5, Name = "Nieodpowiednia/obraźliwa treść", ReportCategory = QUESTION },
					new ReportType{ Id = 6, Name = "Pytanie pojawiło się już na stronie/spam", ReportCategory = QUESTION },
					new ReportType{ Id = 7, Name = "Treść nie związana z tagiem", ReportCategory = QUESTION },
					new ReportType{ Id = 8, Name = "Kradzież zasobów intelektualnych", ReportCategory = QUESTION },
					new ReportType{ Id = 9, Name = "Inne", ReportCategory = QUESTION },
					new ReportType{ Id = 10, Name = "Nieodpowiednia/obraźliwa treść", ReportCategory = ANSWER },
					new ReportType{ Id = 11, Name = "Spam", ReportCategory = ANSWER },
					new ReportType{ Id = 12, Name = "Treść nie nawiązująca do pytania", ReportCategory = ANSWER },
					new ReportType{ Id = 13, Name = "Inne", ReportCategory = ANSWER }
				);
		}
}