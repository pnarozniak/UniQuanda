using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    public partial class UniqueIndexForMainEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersEmails_IdUser",
                table: "UsersEmails");

            migrationBuilder.CreateIndex(
                name: "IX_UsersEmails_IdUser_IsMain",
                table: "UsersEmails",
                columns: new[] { "IdUser", "IsMain" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersEmails_IdUser_IsMain",
                table: "UsersEmails");

            migrationBuilder.CreateIndex(
                name: "IX_UsersEmails_IdUser",
                table: "UsersEmails",
                column: "IdUser");
        }
    }
}
