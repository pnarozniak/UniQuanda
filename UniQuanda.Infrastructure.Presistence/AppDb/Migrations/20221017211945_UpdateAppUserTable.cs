using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class UpdateAppUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Questions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "AppUsers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutText",
                table: "AppUsers",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SemanticScholarProfile",
                table: "AppUsers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AboutText", "Avatar", "Banner", "Birthdate", "City", "FirstName", "LastName", "Nickname", "PhoneNumber", "SemanticScholarProfile" },
                values: new object[] { 1, null, null, null, null, null, "Roman", null, "Programista", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "AboutText",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "SemanticScholarProfile",
                table: "AppUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Questions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "AppUsers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }
    }
}
