using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class Updated_appUser_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutText",
                table: "AppUsers",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AppUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Banner",
                table: "AppUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SemanticScholarProfile",
                table: "AppUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutText",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Banner",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "SemanticScholarProfile",
                table: "AppUsers");
        }
    }
}
