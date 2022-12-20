
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations
{
    public class RoleEfConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(at => at.Id);
            builder.Property(at => at.Id).ValueGeneratedOnAdd();
            builder.Property(at => at.Name).IsRequired();
            builder.HasIndex(r => r.Name).IsUnique();
        }
    }
}
