using Microsoft.EntityFrameworkCore;
using UniQuanda.Infrastructure.Presistence.AuthDb.EfConfigurations;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TempUser> TempUsers { get; set; }
        public virtual DbSet<UserEmail> UsersEmails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEfConfiguration());
            modelBuilder.ApplyConfiguration(new TempUserEfConfiguration());
            modelBuilder.ApplyConfiguration(new UserEmailEfConfiguration());
        }
    }
}