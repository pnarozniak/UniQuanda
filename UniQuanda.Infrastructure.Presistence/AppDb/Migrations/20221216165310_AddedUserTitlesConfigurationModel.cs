using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class AddedUserTitlesConfigurationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .Annotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .Annotation("Npgsql:Enum:product_type_enum", "premium")
                .Annotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .Annotation("Npgsql:Enum:title_request_status_enum", "accepted,rejected,pending,action_required")
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .OldAnnotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .OldAnnotation("Npgsql:Enum:product_type_enum", "premium")
                .OldAnnotation("Npgsql:Enum:report_category_enum", "user,question,answer");

            migrationBuilder.CreateTable(
                name: "TitleRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleRequestStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "text", nullable: true),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    AcademicTitleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitleRequests_AcademicTitles_AcademicTitleId",
                        column: x => x.AcademicTitleId,
                        principalTable: "AcademicTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TitleRequests_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TitleRequests_AcademicTitleId",
                table: "TitleRequests",
                column: "AcademicTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleRequests_AppUserId",
                table: "TitleRequests",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TitleRequests");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .Annotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .Annotation("Npgsql:Enum:product_type_enum", "premium")
                .Annotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .OldAnnotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .OldAnnotation("Npgsql:Enum:product_type_enum", "premium")
                .OldAnnotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .OldAnnotation("Npgsql:Enum:title_request_status_enum", "accepted,rejected,pending,action_required");
        }
    }
}
