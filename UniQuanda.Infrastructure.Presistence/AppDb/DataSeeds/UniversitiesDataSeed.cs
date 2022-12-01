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
                    Logo = "https://pja.edu.pl/templates/pjwstk/favicon.ico"
                },
                new University
                {
                    Id = 2,
                    Name = "Uniwersytet śląski w Katowicach",
                    Logo = "https://us.edu.pl/wp-content/uploads/strona-g%C5%82%C3%B3wna/favicon/cropped-favicon_navy_white-32x32.png"
                },
                new University
                {
                    Id = 3,
                    Name = "Politechnika Warszawska",
                    Logo = "https://www.pw.edu.pl/design/pw/images/favicon.ico"
                }
            );
        }
    }
}