﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class AppUserEfConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nickname).HasMaxLength(30).IsRequired(false);
        builder.HasIndex(u => u.Nickname).IsUnique();

        builder.Property(u => u.FirstName).HasMaxLength(35).IsRequired(false);

        builder.Property(u => u.LastName).HasMaxLength(51).IsRequired(false);

        builder.Property(u => u.Birthdate).IsRequired(false);

        builder.Property(u => u.Contact).HasMaxLength(22).IsRequired(false);

        builder.Property(u => u.SemanticScholarProfile).IsRequired(false);

        builder.Property(u => u.AboutText).HasMaxLength(4000).IsRequired(false);

        builder.Property(u => u.Avatar).IsRequired(false);

        builder.Property(u => u.Banner).IsRequired(false);

        builder.Property(u => u.City).HasMaxLength(57).IsRequired(false);

        builder.HasMany(u => u.AppUserInUniversities)
            .WithOne(uu => uu.AppUserIdNavigation)
            .HasForeignKey(u => u.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.AppUserTitles)
            .WithOne(at => at.AppUserIdNavigation)
            .HasForeignKey(at => at.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.AppUserAnswersInteractions)
            .WithOne(uai => uai.AppUserIdNavigation)
            .HasForeignKey(uai => uai.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.AppUserQuestionsInteractions)
            .WithOne(uqi => uqi.AppUserIdNavigation)
            .HasForeignKey(uqi => uqi.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.UserPointsInTags)
            .WithOne(ut => ut.AppUserIdNavigation)
            .HasForeignKey(ut => ut.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.GlobalRankingNavigation)
            .WithOne(gr => gr.AppUserIdNavigation)
            .HasForeignKey<GlobalRanking>(gr => gr.AppUserId)
            .OnDelete(DeleteBehavior.SetNull);

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            var users = new List<AppUser>
            {
                new AppUser { Id = 1, Nickname = "Programista", FirstName="Roman" }
            };

            builder.HasData(users);
        }
    }
}