using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UniQuanda.Core.Domain.Enums;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class AddedUserProfileTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AppUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Banner",
                table: "AppUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AcademicTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AcademicTitleType = table.Column<AcademicTitleEnum>(type: "academic_title_enum", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ParentTagId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Tags_ParentTagId",
                        column: x => x.ParentTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsersTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcademicTitleId = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsersTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsersTitles_AcademicTitles_AcademicTitleId",
                        column: x => x.AcademicTitleId,
                        principalTable: "AcademicTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsersTitles_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentQuestionId = table.Column<int>(type: "integer", nullable: false),
                    ParentAnswerId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Answers_ParentAnswerId",
                        column: x => x.ParentAnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_ParentQuestionId",
                        column: x => x.ParentQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsersQuestionsInteractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    IsCreator = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsersQuestionsInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsersQuestionsInteractions_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsersQuestionsInteractions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagsInQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsInQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagsInQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagsInQuestions_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersPointsInTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPointsInTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersPointsInTags_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersPointsInTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsersInUniversities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UniversityId = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsersInUniversities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsersInUniversities_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsersInUniversities_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsersAnswersInteractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnswerId = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    IsCreator = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsersAnswersInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsersAnswersInteractions_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsersAnswersInteractions_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicTitles_AcademicTitleType_Name",
                table: "AcademicTitles",
                columns: new[] { "AcademicTitleType", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ParentAnswerId",
                table: "Answers",
                column: "ParentAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ParentQuestionId",
                table: "Answers",
                column: "ParentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersAnswersInteractions_AnswerId",
                table: "AppUsersAnswersInteractions",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersAnswersInteractions_AppUserId_AnswerId",
                table: "AppUsersAnswersInteractions",
                columns: new[] { "AppUserId", "AnswerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersInUniversities_AppUserId",
                table: "AppUsersInUniversities",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersInUniversities_Order_AppUserId",
                table: "AppUsersInUniversities",
                columns: new[] { "Order", "AppUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersInUniversities_UniversityId_AppUserId",
                table: "AppUsersInUniversities",
                columns: new[] { "UniversityId", "AppUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersQuestionsInteractions_AppUserId_QuestionId",
                table: "AppUsersQuestionsInteractions",
                columns: new[] { "AppUserId", "QuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersQuestionsInteractions_QuestionId",
                table: "AppUsersQuestionsInteractions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersTitles_AcademicTitleId",
                table: "AppUsersTitles",
                column: "AcademicTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersTitles_AppUserId_AcademicTitleId",
                table: "AppUsersTitles",
                columns: new[] { "AppUserId", "AcademicTitleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersTitles_AppUserId_Order",
                table: "AppUsersTitles",
                columns: new[] { "AppUserId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ParentTagId",
                table: "Tags",
                column: "ParentTagId");

            migrationBuilder.CreateIndex(
                name: "IX_TagsInQuestions_QuestionId_Order",
                table: "TagsInQuestions",
                columns: new[] { "QuestionId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagsInQuestions_QuestionId_TagId",
                table: "TagsInQuestions",
                columns: new[] { "QuestionId", "TagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagsInQuestions_TagId",
                table: "TagsInQuestions",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPointsInTags_AppUserId_TagId",
                table: "UsersPointsInTags",
                columns: new[] { "AppUserId", "TagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersPointsInTags_TagId",
                table: "UsersPointsInTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsersAnswersInteractions");

            migrationBuilder.DropTable(
                name: "AppUsersInUniversities");

            migrationBuilder.DropTable(
                name: "AppUsersQuestionsInteractions");

            migrationBuilder.DropTable(
                name: "AppUsersTitles");

            migrationBuilder.DropTable(
                name: "TagsInQuestions");

            migrationBuilder.DropTable(
                name: "UsersPointsInTags");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropTable(
                name: "AcademicTitles");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Banner",
                table: "AppUsers");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic");
        }
    }
}
