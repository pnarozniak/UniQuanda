using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UniQuanda.Core.Domain.Enums;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class AddedReportsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .Annotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic");

            migrationBuilder.CreateTable(
                name: "ReportTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ReportCategory = table.Column<ReportCategoryEnum>(type: "report_category_enum", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReportTypeId = table.Column<int>(type: "integer", nullable: false),
                    ReporterId = table.Column<int>(type: "integer", nullable: false),
                    ReportedAnswerId = table.Column<int>(type: "integer", nullable: true),
                    ReportedQuestionId = table.Column<int>(type: "integer", nullable: true),
                    ReportedUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Answers_ReportedAnswerId",
                        column: x => x.ReportedAnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_AppUsers_ReportedUserId",
                        column: x => x.ReportedUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_AppUsers_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Questions_ReportedQuestionId",
                        column: x => x.ReportedQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_ReportTypes_ReportTypeId",
                        column: x => x.ReportTypeId,
                        principalTable: "ReportTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportedAnswerId",
                table: "Reports",
                column: "ReportedAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportedQuestionId",
                table: "Reports",
                column: "ReportedQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportedUserId",
                table: "Reports",
                column: "ReportedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReporterId",
                table: "Reports",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportTypeId",
                table: "Reports",
                column: "ReportTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "ReportTypes");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .OldAnnotation("Npgsql:Enum:report_category_enum", "user,question,answer");
        }
    }
}
