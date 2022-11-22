using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using static UniQuanda.Core.Domain.Enums.AcademicTitleEnum;

namespace UniQuanda.Infrastructure.Presistence.AppDb.DataSeeds
{
    public class AcademicTitleDataSeed : IEntityTypeConfiguration<AcademicTitle>
    {
        public void Configure(EntityTypeBuilder<AcademicTitle> builder)
        {
            builder.HasData(
                new AcademicTitle { Id = 1, Name = "inż.", AcademicTitleType = Engineer },
                new AcademicTitle { Id = 2, Name = "mgr inż.", AcademicTitleType = Engineer },
                new AcademicTitle { Id = 3, Name = "dr inż.", AcademicTitleType = Engineer },
                new AcademicTitle { Id = 4, Name = "dr hab. inż.", AcademicTitleType = Engineer },
                new AcademicTitle { Id = 5, Name = "prof.", AcademicTitleType = Engineer },
                new AcademicTitle { Id = 6, Name = "lic.", AcademicTitleType = Bachelor },
                new AcademicTitle { Id = 7, Name = "mgr", AcademicTitleType = Bachelor },
                new AcademicTitle { Id = 8, Name = "dr", AcademicTitleType = Bachelor },
                new AcademicTitle { Id = 9, Name = "dr hab.", AcademicTitleType = Bachelor },
                new AcademicTitle { Id = 10, Name = "prof.", AcademicTitleType = Bachelor },
                new AcademicTitle { Id = 11, Name = "prof. PJATK", AcademicTitleType = Academic },
                new AcademicTitle { Id = 12, Name = "prof. UŚ", AcademicTitleType = Academic },
                new AcademicTitle { Id = 13, Name = "prof. PW", AcademicTitleType = Academic }
                );
        }
    }
}
