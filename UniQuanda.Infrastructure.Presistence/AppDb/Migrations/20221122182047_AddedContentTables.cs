using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class AddedContentTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .Annotation("Npgsql:Enum:content_text_type_enum", "html,la_te_x,markdown")
                .Annotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .Annotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .OldAnnotation("Npgsql:Enum:report_category_enum", "user,question,answer");

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 22, 19, 20, 47, 364, DateTimeKind.Local).AddTicks(5746));

            migrationBuilder.AddColumn<string>(
                name: "Header",
                table: "Questions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ViewsCount",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Answers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RawText = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    HtmlText = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<int>(type: "integer", nullable: false),
                    ContentTextType = table.Column<int>(type: "integer", nullable: false),
                    SearchVector = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: false)
                        .Annotation("Npgsql:TsVectorConfig", "polish")
                        .Annotation("Npgsql:TsVectorProperties", new[] { "Text" })
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    URL = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImagesInContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageId = table.Column<int>(type: "integer", nullable: false),
                    ContentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesInContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagesInContent_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagesInContent_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ContentId",
                table: "Questions",
                column: "ContentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ContentId",
                table: "Answers",
                column: "ContentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contents_SearchVector",
                table: "Contents",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesInContent_ContentId",
                table: "ImagesInContent",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesInContent_ImageId",
                table: "ImagesInContent",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Contents_ContentId",
                table: "Answers",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Contents_ContentId",
                table: "Questions",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Contents_ContentId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Contents_ContentId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "ImagesInContent");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ContentId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_ContentId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Header",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ViewsCount",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Answers");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .Annotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .OldAnnotation("Npgsql:Enum:content_text_type_enum", "html,la_te_x,markdown")
                .OldAnnotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .OldAnnotation("Npgsql:Enum:report_category_enum", "user,question,answer");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Description", "ImageUrl", "IsDeleted", "Name", "ParentTagId" },
                values: new object[] { 125, null, "https://dev.pl:2002/api/Image/Tags/filozofia.jpg", false, "Filozofia", 8 });
        }
    }
}
