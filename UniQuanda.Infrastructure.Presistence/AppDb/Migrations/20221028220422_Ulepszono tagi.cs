using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class Ulepszonotagi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tags",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Tags",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Tags",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "polish")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Name", "Description" });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SearchVector",
                table: "Tags",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_SearchVector",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "Tags");
        }
    }
}
