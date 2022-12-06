using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class NicknameIsNowNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AppUsers",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AppUsers",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Description", "ImageUrl", "IsDeleted", "Name", "ParentTagId" },
                values: new object[] { 125, null, "https://dev.pl:2002/api/Image/Tags/filozofia.jpg", false, "Filozofia", 8 });
        }
    }
}
