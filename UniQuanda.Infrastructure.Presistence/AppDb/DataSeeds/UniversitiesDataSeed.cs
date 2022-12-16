using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.DataSeeds
{
    public class UniversitiesDataSeed : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.HasData(
                new University()
                {
                    Id = 1,
                    Name = "Polsko-Japońska Akademia Technik Komputerowych",
                    Icon = "https://dev.uniquanda.pl:2002/api/Image/University/1/icon.jpg",
                    Logo = "https://dev.uniquanda.pl:2002/api/Image/University/1/logo.jpg",
                    Contact = "Email: pjatk@pja.edu.pl",
                    Regex = "(@(pjwstk|pja)\\.edu\\.pl$)"
                },
                new University
                {
                    Id = 2,
                    Name = "Uniwersytet śląski w Katowicach",
                    Icon = "https://dev.uniquanda.pl:2002/api/Image/University/2/icon.jpg",
                    Logo = "https://dev.uniquanda.pl:2002/api/Image/University/2/logo.jpg",
                    Contact = "E-mail: info@us.edu.pl",
                    Regex = "(@.*us\\.edu\\.pl$)"
                },
                new University
                {
                    Id = 3,
                    Name = "Politechnika Warszawska",
                    Icon = "https://dev.uniquanda.pl:2002/api/Image/University/3/icon.jpg",
                    Logo = "https://dev.uniquanda.pl:2002/api/Image/University/3/logo.jpg",
                    Contact = "Tel. (22) 234 7211",
                    Regex = "(@.pw\\.edu\\.pl$)"
                }
            );
        }
    }
}