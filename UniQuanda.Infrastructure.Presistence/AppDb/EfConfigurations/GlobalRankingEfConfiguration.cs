using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class GlobalRankingEfConfiguration : IEntityTypeConfiguration<GlobalRanking>
{
    public void Configure(EntityTypeBuilder<GlobalRanking> builder)
    {
        builder.HasKey(gr => gr.Place);

        builder.Property(gr => gr.Points).IsRequired();
    }
}