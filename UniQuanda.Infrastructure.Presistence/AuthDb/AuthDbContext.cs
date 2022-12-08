using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Enums.DbModel;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using UniQuanda.Infrastructure.Presistence.AuthDb.EfConfigurations;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<TempUser> TempUsers { get; set; }
    public virtual DbSet<UserEmail> UsersEmails { get; set; }
    public virtual DbSet<UserActionToConfirm> UsersActionsToConfirm { get; set; }
    public virtual DbSet<OAuthUser> OAuthUsers { get; set; }
    public virtual DbSet<PremiumPayment> PremiumPayments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEfConfiguration());
        modelBuilder.ApplyConfiguration(new TempUserEfConfiguration());
        modelBuilder.ApplyConfiguration(new UserEmailEfConfiguration());
        modelBuilder.ApplyConfiguration(new UserActionToConfirmEfConfiguration());
        modelBuilder.ApplyConfiguration(new OAuthUserEfConfiguration());
        modelBuilder.ApplyConfiguration(new PremiumPaymentEfConfiguration());

        modelBuilder.HasPostgresEnum<UserActionToConfirmEnum>();
        modelBuilder.HasPostgresEnum<OAuthProviderEnum>();
        modelBuilder.HasPostgresEnum<PremiumPaymentStatusEnum>();
    }
}