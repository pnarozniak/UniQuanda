using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class AddedIconsAndLogosToUniversity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Universities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Icon", "Logo" },
                values: new object[] { "https://dev.uniquanda.pl:2002/api/Image/University/1/icon.jpg", "https://dev.uniquanda.pl:2002/api/Image/University/1/logo.jpg" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Icon", "Logo" },
                values: new object[] { "https://dev.uniquanda.pl:2002/api/Image/University/2/icon.jpg", "https://dev.uniquanda.pl:2002/api/Image/University/2/logo.jpg" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Icon", "Logo", "Regex" },
                values: new object[] { "https://dev.uniquanda.pl:2002/api/Image/University/3/icon.jpg", "https://dev.uniquanda.pl:2002/api/Image/University/3/logo.jpg", "(@.pw\\.edu\\.pl$)" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Universities");

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Logo",
                value: "https://pja.edu.pl/templates/pjwstk/favicon.ico");

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Logo",
                value: "https://us.edu.pl/wp-content/uploads/strona-g%C5%82%C3%B3wna/favicon/cropped-favicon_navy_white-32x32.png");

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Logo", "Regex" },
                values: new object[] { "https://www.pw.edu.pl/design/pw/images/favicon.ico", "(@pw\\.edu\\.pl$)" });
        }
    }
}
