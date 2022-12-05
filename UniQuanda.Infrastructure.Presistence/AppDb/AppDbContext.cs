using Microsoft.EntityFrameworkCore;
using Npgsql;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AppDb.DataSeeds;
using UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb;

public class AppDbContext : DbContext
{
    static AppDbContext() => NpgsqlConnection.GlobalTypeMapper
        .MapEnum<AcademicTitleEnum>()
        .MapEnum<ReportCategoryEnum>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<AcademicTitle> AcademicTitles { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<AppUserInUniversity> AppUsersInUniversities { get; set; }
    public DbSet<AppUserTitle> AppUsersTitles { get; set; }
    public DbSet<AppUserQuestionInteraction> AppUsersQuestionsInteractions { get; set; }
    public DbSet<AppUserAnswerInteraction> AppUsersAnswersInteractions { get; set; }
    public DbSet<UserPointsInTag> UsersPointsInTags { get; set; }
    public DbSet<TagInQuestion> TagsInQuestions { get; set; }
    public DbSet<ReportType> ReportTypes { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<ImageInContent> ImagesInContent { get; set; }
    public DbSet<Image> Images { get; set; }

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

        modelBuilder.HasPostgresEnum<AcademicTitleEnum>();
        modelBuilder.HasPostgresEnum<ReportCategoryEnum>();
        modelBuilder.HasPostgresEnum<ContentTypeEnum>();

        modelBuilder.ApplyConfiguration(new ReportTypeDataSeed());
        modelBuilder.ApplyConfiguration(new AcademicTitleDataSeed());
        modelBuilder.ApplyConfiguration(new UniversitiesDataSeed());
        modelBuilder.ApplyConfiguration(new TagDataSeed());

        modelBuilder.HasSequence<int>("SequenceContentId").StartsAt(0).IncrementsBy(1);
    }
}