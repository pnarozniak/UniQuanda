using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class AddedUniversitiesDataAndChangedPhoneToContant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "AppUsers",
                newName: "Contact");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Universities",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Regex",
                table: "Universities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Contact", "Regex" },
                values: new object[] { "Email: pjatk@pja.edu.pl", "(@(pjwstk|pja)\\.edu\\.pl$)" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Contact", "Regex" },
                values: new object[] { "E-mail: info@us.edu.pl", "(@.*us\\.edu\\.pl$)" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Contact", "Regex" },
                values: new object[] { "Tel. (22) 234 7211", "(@pw\\.edu\\.pl$)" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Universities");

            migrationBuilder.DropColumn(
                name: "Regex",
                table: "Universities");

            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "AppUsers",
                newName: "PhoneNumber");
        }
    }
}
