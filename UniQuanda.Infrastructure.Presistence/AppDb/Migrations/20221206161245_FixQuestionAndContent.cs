using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class FixQuestionAndContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentTextType",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "HtmlText",
                table: "Contents");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .Annotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .Annotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .OldAnnotation("Npgsql:Enum:content_text_type_enum", "html,la_te_x,markdown")
                .OldAnnotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .OldAnnotation("Npgsql:Enum:report_category_enum", "user,question,answer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2022, 11, 22, 19, 20, 47, 364, DateTimeKind.Local).AddTicks(5746));

            migrationBuilder.AddColumn<bool>(
                name: "IsFollowing",
                table: "AppUsersQuestionsInteractions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsViewed",
                table: "AppUsersQuestionsInteractions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "IntFunctionWrapper",
                columns: table => new
                {
                    result = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntFunctionWrapper");

            migrationBuilder.DropColumn(
                name: "IsFollowing",
                table: "AppUsersQuestionsInteractions");

            migrationBuilder.DropColumn(
                name: "IsViewed",
                table: "AppUsersQuestionsInteractions");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .Annotation("Npgsql:Enum:content_text_type_enum", "html,la_te_x,markdown")
                .Annotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .Annotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .OldAnnotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .OldAnnotation("Npgsql:Enum:report_category_enum", "user,question,answer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 22, 19, 20, 47, 364, DateTimeKind.Local).AddTicks(5746),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "ContentTextType",
                table: "Contents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HtmlText",
                table: "Contents",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
