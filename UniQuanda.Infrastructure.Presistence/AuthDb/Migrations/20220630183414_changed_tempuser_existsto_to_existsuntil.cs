using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    public partial class changed_tempuser_existsto_to_existsuntil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExistsTo",
                table: "TempUsers",
                newName: "ExistsUntil");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExistsUntil",
                table: "TempUsers",
                newName: "ExistsTo");
        }
    }
}
