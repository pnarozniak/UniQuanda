using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations
{
    internal class LogEfConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();
            
            builder.Property(l => l.Exception).IsRequired(true);
            builder.Property(l => l.StackTrace).IsRequired(true);
            builder.Property(l => l.Endpoint).IsRequired(true);
            builder.Property(l => l.Body);
            builder.Property(l => l.Client);
            builder.Property(l => l.QueryParams);
            builder.Property(l => l.Headers).IsRequired(true);
            builder.Property(l => l.CreatedAt).IsRequired(true);
        }
    }
}
