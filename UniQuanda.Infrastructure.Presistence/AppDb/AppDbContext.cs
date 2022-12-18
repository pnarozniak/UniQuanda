using Microsoft.EntityFrameworkCore;
using Npgsql;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Enums.DbModel;
using UniQuanda.Infrastructure.Presistence.AppDb.DataSeeds;
using UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;
using UniQuanda.Infrastructure.Presistence.AppDb.Functions;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb;

public class AppDbContext : DbContext
{
    static AppDbContext() => NpgsqlConnection.GlobalTypeMapper
        .MapEnum<AcademicTitleEnum>()
        .MapEnum<ReportCategoryEnum>()
        .MapEnum<ProductTypeEnum>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public virtual DbSet<AppUser> AppUsers { get; set; }
    public virtual DbSet<Question> Questions { get; set; }
    public virtual DbSet<Answer> Answers { get; set; }
    public virtual DbSet<AcademicTitle> AcademicTitles { get; set; }
    public virtual DbSet<University> Universities { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<AppUserInUniversity> AppUsersInUniversities { get; set; }
    public virtual DbSet<AppUserTitle> AppUsersTitles { get; set; }
    public virtual DbSet<AppUserQuestionInteraction> AppUsersQuestionsInteractions { get; set; }
    public virtual DbSet<AppUserAnswerInteraction> AppUsersAnswersInteractions { get; set; }
    public virtual DbSet<UserPointsInTag> UsersPointsInTags { get; set; }
    public virtual DbSet<TagInQuestion> TagsInQuestions { get; set; }
    public virtual DbSet<ReportType> ReportTypes { get; set; }
    public virtual DbSet<Report> Reports { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Content> Contents { get; set; }
    public virtual DbSet<ImageInContent> ImagesInContent { get; set; }
    public virtual DbSet<Image> Images { get; set; }
    public virtual DbSet<IntFunction> IntFunctionWrapper { get; set; }
    public virtual DbSet<GlobalRanking> GlobalRankings { get; set; }
    public virtual DbSet<TitleRequest> TitleRequests { get; set; }
    public virtual DbSet<PermissionUsageByUser> PermissionUsageByUsers { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<RolePermission> RolePermissions { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AppUserEfConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionEfConfiguration());
        modelBuilder.ApplyConfiguration(new AnswerEfConfiguration());
        modelBuilder.ApplyConfiguration(new AcademicTitleEfConfiguration());
        modelBuilder.ApplyConfiguration(new UniversityEfConfiguration());
        modelBuilder.ApplyConfiguration(new TagEfConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserInUniversityEfConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserTitleEfConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserQuestionInteractionEfConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserAnswerInteractionEfConfiguration());
        modelBuilder.ApplyConfiguration(new UserPointsInTagEfConfiguration());
        modelBuilder.ApplyConfiguration(new TagInQuestionEfConfiguration());
        modelBuilder.ApplyConfiguration(new ReportTypeEfConfiguration());
        modelBuilder.ApplyConfiguration(new ReportEfConfiguration());
        modelBuilder.ApplyConfiguration(new ContentEfConfiguration());
        modelBuilder.ApplyConfiguration(new ImageInContentEfConfiguration());
        modelBuilder.ApplyConfiguration(new ImageEfConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEfConfiguration());
        modelBuilder.ApplyConfiguration(new GlobalRankingEfConfiguration());
        modelBuilder.ApplyConfiguration(new TitleRequestEfConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionUsageByUserEfConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionEfConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEfConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionEfConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleEfConfiguration());

        modelBuilder.HasPostgresEnum<AcademicTitleEnum>();
        modelBuilder.HasPostgresEnum<ReportCategoryEnum>();
        modelBuilder.HasPostgresEnum<ContentTypeEnum>();
        modelBuilder.HasPostgresEnum<ProductTypeEnum>();
        modelBuilder.HasPostgresEnum<TitleRequestStatusEnum>();
        modelBuilder.HasPostgresEnum<RoleNameEnum>();

        modelBuilder.ApplyConfiguration(new ReportTypeDataSeed());
        modelBuilder.ApplyConfiguration(new AcademicTitleDataSeed());
        modelBuilder.ApplyConfiguration(new UniversitiesDataSeed());
        modelBuilder.ApplyConfiguration(new TagDataSeed());
        modelBuilder.ApplyConfiguration(new ProductDataSeed());
        modelBuilder.ApplyConfiguration(new RoleDataSeed());
        modelBuilder.ApplyConfiguration(new PermissionDataSeed());
        modelBuilder.ApplyConfiguration(new RolePermissionDataSeed());

        modelBuilder.Entity<IntFunction>().HasNoKey();

    }
}