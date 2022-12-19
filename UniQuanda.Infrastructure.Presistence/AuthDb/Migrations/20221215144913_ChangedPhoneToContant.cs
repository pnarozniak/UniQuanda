using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    public partial class ChangedPhoneToContant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "TempUsers",
                newName: "Contact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "TempUsers",
                newName: "PhoneNumber");
        }
    }
}
