using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class DataMigration : Migration
    {
        private static string _SQLFileLocation = "./../UniQuanda.Infrastructure.Presistence/AppDb/SQL/";
        
        /// <summary>
        /// SQL files to load after all migrations.
        /// First argument is QUAN task name and seconds is list of file directions.
        /// </summary>
        private static readonly IDictionary<string, IEnumerable<string>> _files = new Dictionary<string, IEnumerable<string>>()
        {
            {
                "QUAN-155",
                new List<string>()
                {
                    "v1.0/QUAN-155/academic-titles.sql",
                    "v1.0/QUAN-155/tags.sql",
                    "v1.0/QUAN-155/universities.sql",
                }
            },
        };
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            foreach (var (taskName, files) in _files)
            {
                foreach (var file in files)
                {
                    var sql = File.ReadAllText($"{_SQLFileLocation}{file}");
                    migrationBuilder.Sql(sql);
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

    }
}
