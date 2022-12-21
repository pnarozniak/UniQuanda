using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class Added_Test_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IdCreator = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_AppUsers_IdCreator",
                        column: x => x.IdCreator,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestsQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdTest = table.Column<int>(type: "integer", nullable: false),
                    IdQuestion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestsQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestsQuestion_Questions_IdQuestion",
                        column: x => x.IdQuestion,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestsQuestion_Tests_IdTest",
                        column: x => x.IdTest,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_IdCreator",
                table: "Tests",
                column: "IdCreator");

            migrationBuilder.CreateIndex(
                name: "IX_TestsQuestion_IdQuestion",
                table: "TestsQuestion",
                column: "IdQuestion");

            migrationBuilder.CreateIndex(
                name: "IX_TestsQuestion_IdTest",
                table: "TestsQuestion",
                column: "IdTest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestsQuestion");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
