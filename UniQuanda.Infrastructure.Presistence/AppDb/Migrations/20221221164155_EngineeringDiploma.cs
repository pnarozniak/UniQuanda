using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Enums.DbModel;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class EngineeringDiploma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .Annotation("Npgsql:Enum:content_type_enum", "question,answer,message")
                .Annotation("Npgsql:Enum:product_type_enum", "premium")
                .Annotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .Annotation("Npgsql:Enum:title_request_status_enum", "accepted,rejected,pending,action_required");

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
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nickname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: true),
                    LastName = table.Column<string>(type: "character varying(51)", maxLength: 51, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Contact = table.Column<string>(type: "character varying(22)", maxLength: 22, nullable: true),
                    City = table.Column<string>(type: "character varying(57)", maxLength: 57, nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    Banner = table.Column<string>(type: "text", nullable: true),
                    SemanticScholarProfile = table.Column<string>(type: "text", nullable: true),
                    AboutText = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RawText = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<int>(type: "integer", nullable: false),
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
                name: "IntFunctionWrapper",
                columns: table => new
                {
                    result = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Exception = table.Column<string>(type: "text", nullable: false),
                    StackTrace = table.Column<string>(type: "text", nullable: false),
                    Endpoint = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: true),
                    Client = table.Column<string>(type: "text", nullable: true),
                    QueryParams = table.Column<string>(type: "text", nullable: true),
                    Headers = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductType = table.Column<ProductTypeEnum>(type: "product_type_enum", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductType);
                });

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
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ParentTagId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SearchVector = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: false)
                        .Annotation("Npgsql:TsVectorConfig", "polish")
                        .Annotation("Npgsql:TsVectorProperties", new[] { "Name", "Description" })
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
                    Logo = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    Contact = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Regex = table.Column<string>(type: "text", nullable: false)
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
                name: "GlobalRankings",
                columns: table => new
                {
                    Place = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalRankings", x => x.Place);
                    table.ForeignKey(
                        name: "FK_GlobalRankings_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PremiumPayments",
                columns: table => new
                {
                    IdPayment = table.Column<string>(type: "text", nullable: false),
                    IdTransaction = table.Column<string>(type: "text", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PaymentUrl = table.Column<string>(type: "text", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumPayments", x => x.IdPayment);
                    table.ForeignKey(
                        name: "FK_PremiumPayments_AppUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

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
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ViewsCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ContentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    AcademicTitleId = table.Column<int>(type: "integer", nullable: false),
                    ScanId = table.Column<int>(type: "integer", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_TitleRequests_Images_ScanId",
                        column: x => x.ScanId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionUsageByUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    UsedTimes = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionUsageByUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionUsageByUsers_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionUsageByUsers_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    LimitRefreshPeriod = table.Column<int>(type: "integer", nullable: true),
                    AllowedUsages = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ValidUnitl = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
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
                name: "TestsTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdTest = table.Column<int>(type: "integer", nullable: false),
                    IdTag = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestsTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestsTags_Tags_IdTag",
                        column: x => x.IdTag,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestsTags_Tests_IdTest",
                        column: x => x.IdTest,
                        principalTable: "Tests",
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
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    HasBeenModified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LikeCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                        name: "FK_Answers_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
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
                    IsViewed = table.Column<bool>(type: "boolean", nullable: false),
                    IsFollowing = table.Column<bool>(type: "boolean", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "AppUsersAnswersInteractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnswerId = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    IsCreator = table.Column<bool>(type: "boolean", nullable: false),
                    LikeValue = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.InsertData(
                table: "AcademicTitles",
                columns: new[] { "Id", "AcademicTitleType", "Name" },
                values: new object[,]
                {
                    { 1, AcademicTitleEnum.Engineer, "inż." },
                    { 2, AcademicTitleEnum.Engineer, "mgr inż." },
                    { 3, AcademicTitleEnum.Engineer, "dr inż." },
                    { 4, AcademicTitleEnum.Engineer, "dr hab. inż." },
                    { 5, AcademicTitleEnum.Engineer, "prof." },
                    { 6, AcademicTitleEnum.Bachelor, "lic." },
                    { 7, AcademicTitleEnum.Bachelor, "mgr" },
                    { 8, AcademicTitleEnum.Bachelor, "dr" },
                    { 9, AcademicTitleEnum.Bachelor, "dr hab." },
                    { 10, AcademicTitleEnum.Bachelor, "prof." },
                    { 11, AcademicTitleEnum.Academic, "prof. PJATK" },
                    { 12, AcademicTitleEnum.Academic, "prof. UŚ" },
                    { 13, AcademicTitleEnum.Academic, "prof. PW" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AboutText", "Avatar", "Banner", "Birthdate", "City", "Contact", "FirstName", "LastName", "Nickname", "SemanticScholarProfile" },
                values: new object[] { 1, null, null, null, null, null, null, "Roman", null, "Programista", null });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ask-question" },
                    { 2, "create-course" },
                    { 3, "solve-course" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductType", "Price" },
                values: new object[] { ProductTypeEnum.Premium, 19m });

            migrationBuilder.InsertData(
                table: "ReportTypes",
                columns: new[] { "Id", "Name", "ReportCategory" },
                values: new object[,]
                {
                    { 1, "Podszywanie się pod inną osobę", ReportCategoryEnum.USER },
                    { 2, "Publikowanie niestosownych treści", ReportCategoryEnum.USER },
                    { 3, "Nękanie lub cyberprzemoc", ReportCategoryEnum.USER },
                    { 4, "Inne", ReportCategoryEnum.USER },
                    { 5, "Nieodpowiednia/obraźliwa treść", ReportCategoryEnum.QUESTION },
                    { 6, "Pytanie pojawiło się już na stronie/spam", ReportCategoryEnum.QUESTION },
                    { 7, "Treść nie związana z tagiem", ReportCategoryEnum.QUESTION },
                    { 8, "Kradzież zasobów intelektualnych", ReportCategoryEnum.QUESTION },
                    { 9, "Inne", ReportCategoryEnum.QUESTION },
                    { 10, "Nieodpowiednia/obraźliwa treść", ReportCategoryEnum.ANSWER },
                    { 11, "Spam", ReportCategoryEnum.ANSWER },
                    { 12, "Treść nie nawiązująca do pytania", ReportCategoryEnum.ANSWER },
                    { 13, "Inne", ReportCategoryEnum.ANSWER }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "user" },
                    { 3, "premium" },
                    { 4, "eduUser" },
                    { 5, "titledUser" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "ParentTagId" },
                values: new object[,]
                {
                    { 1, "Programowanie i tematy pokrewne dotyczące informatyki", "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Informatyka", null },
                    { 2, "Zagadnienia z dziedziny matematyki", "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Matematyka", null },
                    { 3, "Zagadnienia z dziedziny fizyki", "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka.jpg", "Fizyka", null },
                    { 4, "Zagadnienia z dziedziny chemii", "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia.jpg", "Chemia", null },
                    { 5, "Zagadnienia z dziedziny biologii", "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia.jpg", "Biologia", null },
                    { 6, "Zagadnienia z dziedziny geografii", "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia.jpg", "Geografia", null },
                    { 7, "Zagadnienia z dziedziny historii", "https://dev.uniquanda.pl:2002/api/Image/Tags/historia.jpg", "Historia", null },
                    { 8, "Zagadnienia z dziedziny filozofii", "https://dev.uniquanda.pl:2002/api/Image/Tags/filozofia.jpg", "Filozofia", null },
                    { 9, "Zagadnienia z dziedziny psychologii", "https://dev.uniquanda.pl:2002/api/Image/Tags/pscyhologia.jpg", "Psychologia", null },
                    { 10, "Zagadnienia z dziedziny dziedzin sztuki", "https://dev.uniquanda.pl:2002/api/Image/Tags/sztuka.jpg", "Sztuka", null },
                    { 11, "Zagadnienia z dziedziny języków obcych", "https://dev.uniquanda.pl:2002/api/Image/Tags/filologia.jpg", "Filologia", null },
                    { 12, "Zagadnienia z dziedziny polskiego prawa", "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo", null },
                    { 13, "Zagadnienia z dziedziny międzynarodowego prawa", "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo_międzynarodowe.jpg", "Prawo międzynarodowe", null },
                    { 14, "Zagadnienia z dziedziny medycyny", "https://dev.uniquanda.pl:2002/api/Image/Tags/medycyna.jpg", "Medycyna", null },
                    { 15, "Zagadnienia z dziedziny inżynierii", "https://dev.uniquanda.pl:2002/api/Image/Tags/inżynieria.jpg", "Inżyneria", null },
                    { 16, "Zagadnienia z dziedziny ekonomii i finansów", "https://dev.uniquanda.pl:2002/api/Image/Tags/ekonomia.jpg", "Ekonomia", null },
                    { 17, "Nauki polityczne, nauki o polityce, nauka o polityce. Nauka społeczna zajmująca się działalnością związaną ze sprawowaniem władzy polityczne", "https://dev.uniquanda.pl:2002/api/Image/Tags/politologia.jpg", "Politologia", null },
                    { 18, "Zagadnienia z dziedziny religii", "https://dev.uniquanda.pl:2002/api/Image/Tags/teologia.jpg", "Teologia", null },
                    { 19, "Zagadnienia z dziedziny edukacji i wychowania", "https://dev.uniquanda.pl:2002/api/Image/Tags/pedagogika.jpg", "Pedagogika", null },
                    { 20, "Zagadnienia z dziedziny sportu", "https://dev.uniquanda.pl:2002/api/Image/Tags/sport.jpg", "Sport", null },
                    { 21, "Zagadnienia z dziedziny turystyki i podróży", "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka.jpg", "Turystyka", null },
                    { 22, "Zagadnienia z dziedziny logistyki i transportu", "https://dev.uniquanda.pl:2002/api/Image/Tags/logistyka.jpg", "Logistyka", null },
                    { 23, "Zagadnienia z dziedziny marketingu i reklamy", "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing", null },
                    { 24, "Zagadnienia z dziedziny dziennikarstwa", "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo.jpg", "Dziennikarstwo", null },
                    { 25, "Zagadnienia z dziedziny architektury", "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura.jpg", "Architektura", null },
                    { 26, "Zagadnienia związane z lotnictwem", "https://dev.uniquanda.pl:2002/api/Image/Tags/lotnictwo.jpg", "Lotnictwo", null }
                });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "Contact", "Icon", "Logo", "Name", "Regex" },
                values: new object[,]
                {
                    { 1, "Email: pjatk@pja.edu.pl", "https://dev.uniquanda.pl:2002/api/Image/University/1/icon.jpg", "https://dev.uniquanda.pl:2002/api/Image/University/1/logo.jpg", "Polsko-Japońska Akademia Technik Komputerowych", "(@(pjwstk|pja)\\.edu\\.pl$)" },
                    { 2, "E-mail: info@us.edu.pl", "https://dev.uniquanda.pl:2002/api/Image/University/2/icon.jpg", "https://dev.uniquanda.pl:2002/api/Image/University/2/logo.jpg", "Uniwersytet śląski w Katowicach", "(@.*us\\.edu\\.pl$)" },
                    { 3, "Tel. (22) 234 7211", "https://dev.uniquanda.pl:2002/api/Image/University/3/icon.jpg", "https://dev.uniquanda.pl:2002/api/Image/University/3/logo.jpg", "Politechnika Warszawska", "(@.pw\\.edu\\.pl$)" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "AllowedUsages", "LimitRefreshPeriod", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, null, null, 1, 1 },
                    { 2, 3, 604800, 1, 2 },
                    { 3, null, null, 1, 3 },
                    { 4, 5, 604800, 1, 4 },
                    { 5, 3, 604800, 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "ParentTagId" },
                values: new object[,]
                {
                    { 44, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/inzynieria_oprogramowania.jpg", "Inżynieria oprogramowania", 1 },
                    { 45, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Architektura oprogramowania", 1 },
                    { 46, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Testowanie oprogramowania", 1 },
                    { 47, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/sztuczna_inteligencja.jpg", "Sztuczna inteligencja", 1 },
                    { 48, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie obiektowe", 1 },
                    { 49, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie funkcyjne", 1 },
                    { 50, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie proceduralne", 1 },
                    { 51, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie zdarzeniowe", 1 },
                    { 52, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie asynchroniczne", 1 },
                    { 53, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie równoległe", 1 },
                    { 54, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie wielowątkowe", 1 },
                    { 55, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie wieloprocesowe", 1 },
                    { 56, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie wielokomputerowe", 1 },
                    { 57, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie rozproszone", 1 },
                    { 58, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie równoległe", 1 },
                    { 59, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg", "Programowanie wielowątkowe", 1 },
                    { 60, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Matematyka dyskretna", 2 },
                    { 61, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/planimetria.jpg", "Planimetria", 2 },
                    { 62, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza matematyczna", 2 },
                    { 63, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Algebra liniowa", 2 },
                    { 64, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/geometria.jpg", "Geometria", 2 },
                    { 65, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/logika_matematyczna.jpg", "Logika matematyczna", 2 },
                    { 66, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/teoria_liczb.jpg", "Teoria liczb", 2 },
                    { 67, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Teoria mnogości", 2 },
                    { 68, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Teoria grafów", 2 },
                    { 69, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/teoria_gier.jpg", "Teoria gier", 2 },
                    { 70, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/teoria_kodowania.jpg", "Teoria kodowania", 2 },
                    { 71, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/teoria_obliczeń.jpg", "Teoria obliczeń", 2 },
                    { 72, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Teoria algorytmów", 2 },
                    { 73, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Algebra nieliniowa", 2 },
                    { 74, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/staystyka.jpg", "Staystyka", 2 },
                    { 75, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Matematyka stosowana", 2 },
                    { 76, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Topologia", 2 },
                    { 77, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza numeryczna", 2 },
                    { 78, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza funkcjonalna", 2 },
                    { 79, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza zespolona", 2 },
                    { 80, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza Fouriera", 2 },
                    { 81, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza graniczna", 2 },
                    { 82, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza różnicowa", 2 },
                    { 83, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza integralna", 2 },
                    { 84, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza funkcji wielu zmiennych", 2 },
                    { 85, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza funkcji rzeczywistych", 2 },
                    { 86, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg", "Analiza funkcji zespolonych", 2 },
                    { 87, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka_kwantowa.jpg", "Fizyka Kwantowa", 3 },
                    { 88, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka.jpg", "Fizyka atomowa", 3 },
                    { 89, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka.jpg", "Fizyka molekularna", 3 },
                    { 90, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka_ciała_stałego.jpg", "Fizyka ciała stałego", 3 },
                    { 91, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka_ciała_płynnego.jpg", "Fizyka ciała płynnego", 3 },
                    { 92, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka_ciała_gazowego.jpg", "Fizyka ciała gazowego", 3 },
                    { 93, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka_ciała_promieniotwórczego.jpg", "Fizyka ciała promieniotwórczego", 3 },
                    { 94, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka.jpg", "Fizyka ciała złożonego", 3 },
                    { 95, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia_organiczna.jpg", "Chemia organiczna", 4 },
                    { 96, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia_analityczna.jpg", "Chemia analityczna", 4 },
                    { 97, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia.jpg", "Chemia fizyczna", 4 },
                    { 98, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia.jpg", "Chemia inorganiczna", 4 },
                    { 99, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia_biologiczna.jpg", "Chemia biologiczna", 4 },
                    { 100, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_molekularna.jpg", "Biologia molekularna", 5 },
                    { 101, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_komórkowa.jpg", "Biologia komórkowa", 5 },
                    { 102, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_ewolucyjna.jpg", "Biologia ewolucyjna", 5 },
                    { 103, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia.jpg", "Biologia systematyczna", 5 },
                    { 104, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia.jpg", "Biologia populacyjna", 5 },
                    { 105, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_ekologiczna.jpg", "Biologia ekologiczna", 5 },
                    { 106, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_roślin.jpg", "Biologia roślin", 5 },
                    { 107, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_zwierząt.jpg", "Biologia zwierząt", 5 },
                    { 108, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/geomorfologia.jpg", "Geomorfologia", 6 },
                    { 109, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/geologia.jpg", "Geologia", 6 },
                    { 110, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia_fizyczna.jpg", "Geografia fizyczna", 6 },
                    { 111, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia.jpg", "Geografia polityczna", 6 },
                    { 112, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia.jpg", "Geografia ekonomiczna", 6 },
                    { 113, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia.jpg", "Geografia społeczna", 6 },
                    { 114, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia_kulturowa.jpg", "Geografia kulturowa", 6 },
                    { 115, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia_regionalna.jpg", "Geografia regionalna", 6 },
                    { 116, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia.jpg", "Geografia turystyki", 6 },
                    { 117, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/historia_starożytna.jpg", "Historia starożytna", 7 },
                    { 118, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/historia_średniowiecza.jpg", "Historia średniowiecza", 7 },
                    { 119, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/historia_nowożytna.jpg", "Historia nowożytna", 7 },
                    { 120, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/historia_współczesna.jpg", "Historia współczesna", 7 },
                    { 121, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/nauki_społeczne.jpg", "Nauki społeczne", 8 },
                    { 122, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/socjologia.jpg", "Socjologia", 8 },
                    { 123, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/antropologia.jpg", "Antropologia", 8 },
                    { 124, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/etnografia.jpg", "Etnografia", 8 },
                    { 125, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/etnologia.jpg", "Etnologia", 8 },
                    { 126, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/etnomuzykologia.jpg", "Etnomuzykologia", 8 },
                    { 127, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_polski.jpg", "Język polski", 11 },
                    { 128, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_angielski.jpg", "Język angielski", 11 },
                    { 129, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_niemiecki.jpg", "Język niemiecki", 11 },
                    { 130, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_rosyjski.jpg", "Język rosyjski", 11 },
                    { 131, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_francuski.jpg", "Język francuski", 11 },
                    { 132, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_hiszpański.jpg", "Język hiszpański", 11 },
                    { 133, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_włoski.jpg", "Język włoski", 11 },
                    { 134, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_japoński.jpg", "Język japoński", 11 },
                    { 135, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_chiński.jpg", "Język chiński", 11 },
                    { 136, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_koreański.jpg", "Język koreański", 11 },
                    { 137, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/język_portugalski.jpg", "Język portugalski", 11 },
                    { 138, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia_poznawcza.jpg", "Psychologia poznawcza", 9 },
                    { 139, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia_rozwojowa.jpg", "Psychologia rozwojowa", 9 },
                    { 140, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia.jpg", "Psychologia społeczna", 9 },
                    { 141, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia_kliniczna.jpg", "Psychologia kliniczna", 9 },
                    { 142, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia_pracy.jpg", "Psychologia pracy", 9 },
                    { 143, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia.jpg", "Psychologia edukacyjna", 9 },
                    { 144, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia_sportu.jpg", "Psychologia sportu", 9 },
                    { 145, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia.jpg", "Psychologia kryminalna", 9 },
                    { 146, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia.jpg", "Psychologia medyczna", 9 },
                    { 147, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/malarstwo.jpg", "Malarstwo", 10 },
                    { 148, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/rzeźba.jpg", "Rzeźba", 10 },
                    { 149, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/grafika.jpg", "Grafika", 10 },
                    { 150, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/fotografia.jpg", "Fotografia", 10 },
                    { 151, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/film.jpg", "Film", 10 },
                    { 152, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/muzyka.jpg", "Muzyka", 10 },
                    { 153, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/teatr.jpg", "Teatr", 10 },
                    { 154, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/literatura.jpg", "Literatura", 10 },
                    { 155, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo podatkowe", 12 },
                    { 156, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo cywilne", 12 },
                    { 157, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo karne", 12 },
                    { 158, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo administracyjne", 12 },
                    { 159, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo pracy", 12 },
                    { 160, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo gospodarcze", 12 },
                    { 161, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo handlowe", 12 },
                    { 162, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo traktatów", 13 },
                    { 163, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo dyplomatyczne", 13 },
                    { 164, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo konsularne", 13 },
                    { 165, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg", "Prawo kosmiczne", 13 },
                    { 166, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/anatomia.jpg", "Anatomia", 14 },
                    { 167, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/chirurgia.jpg", "Chirurgia", 14 },
                    { 168, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/farmakologia.jpg", "Farmakologia", 14 },
                    { 169, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/gastroenterologia.jpg", "Gastroenterologia", 14 },
                    { 170, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/ginekologia.jpg", "Ginekologia", 14 },
                    { 171, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/hematologia.jpg", "Hematologia", 14 },
                    { 172, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/kardiologia.jpg", "Kardiologia", 14 },
                    { 173, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/medycyna_rodzinna.jpg", "Medycyna rodzinna", 14 },
                    { 174, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/medycyna_pracy.jpg", "Medycyna pracy", 14 },
                    { 175, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/automatyka_i_robotyka.jpg", "Automatyka i robotyka", 15 },
                    { 176, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/elektronika.jpg", "Elektronika", 15 },
                    { 177, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/elektrotechnika.jpg", "Elektrotechnika", 15 },
                    { 178, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/mechanika.jpg", "Mechanika", 15 },
                    { 179, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/nauki_o_ziemi.jpg", "Nauki o Ziemi", 15 },
                    { 180, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/inżynieria.jpg", "Nauki o materiałach", 15 },
                    { 181, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/nauki_o_transportie.jpg", "Nauki o transportie", 15 },
                    { 182, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/nauki_o_wodzie.jpg", "Nauki o wodzie", 15 },
                    { 183, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/inżynieria.jpg", "Nauki o zrównoważonym rozwoju", 15 },
                    { 184, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/kryptowaluty.jpg", "Kryptowaluty", 16 },
                    { 185, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/ekonomia.jpg", "Giełda", 16 },
                    { 186, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/finanse.jpg", "Finanse", 16 },
                    { 187, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/podatki.jpg", "Podatki", 16 },
                    { 188, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/polityka_społeczna.jpg", "Polityka społeczna", 17 },
                    { 189, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/polityka.jpg", "Polityka zagraniczna", 17 },
                    { 190, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/europeistyka.jpg", "Europeistyka", 17 },
                    { 191, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/chrześcijaństwo.jpg", "Chrześcijaństwo", 18 },
                    { 192, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/islam.jpg", "Islam", 18 },
                    { 193, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/judaizm.jpg", "Judaizm", 18 },
                    { 194, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/buddyzm.jpg", "Buddyzm", 18 },
                    { 195, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/hinduizm.jpg", "Hinduizm", 18 },
                    { 196, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/teologia.jpg", "Bahaizm", 18 },
                    { 197, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/protestantyzm.jpg", "Protestantyzm", 18 },
                    { 198, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/świadkowie_jehowy.jpg", "Świadkowie Jehowy", 18 },
                    { 199, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_wczesnoszkolne.jpg", "Wychowanie wczesnoszkolne", 19 },
                    { 200, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_przedszkolne.jpg", "Wychowanie przedszkolne", 19 },
                    { 201, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_szkolne.jpg", "Wychowanie szkolne", 19 },
                    { 202, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_gimnazjalne.jpg", "Wychowanie gimnazjalne", 19 },
                    { 203, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_licealne.jpg", "Wychowanie licealne", 19 },
                    { 204, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_studenckie.jpg", "Wychowanie studenckie", 19 },
                    { 205, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_dorosłych.jpg", "Wychowanie dorosłych", 19 },
                    { 206, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_specjalne.jpg", "Wychowanie specjalne", 19 },
                    { 207, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_w_rodzinie.jpg", "Wychowanie w rodzinie", 19 },
                    { 208, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/piłka_nożna.jpg", "Piłka nożna", 20 },
                    { 209, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/koszykówka.jpg", "Koszykówka", 20 },
                    { 210, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/siatkówka.jpg", "Siatkówka", 20 },
                    { 211, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/tenis.jpg", "Tenis", 20 },
                    { 212, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/siatkówka_plażowa.jpg", "Siatkówka plażowa", 20 },
                    { 213, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/piłka_ręczna.jpg", "Piłka ręczna", 20 },
                    { 214, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/piłka_wodna.jpg", "Piłka wodna", 20 },
                    { 215, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/kolarstwo.jpg", "Kolarstwo", 20 },
                    { 216, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/bieganie.jpg", "Bieganie", 20 },
                    { 217, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka_poznawczawa.jpg", "Turystyka poznawczawa", 21 },
                    { 218, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka_rekreacyjna.jpg", "Turystyka rekreacyjna", 21 },
                    { 219, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka_sportowa.jpg", "Turystyka sportowa", 21 },
                    { 220, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka.jpg", "Turystyka biznesowa", 21 },
                    { 221, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka.jpg", "Turystyka medyczna", 21 },
                    { 222, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka_kulturowa.jpg", "Turystyka kulturowa", 21 },
                    { 223, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka.jpg", "Turystyka religijna", 21 },
                    { 224, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/transport_lotniczy.jpg", "Transport lotniczy", 22 },
                    { 225, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/transport_drogowy.jpg", "Transport drogowy", 22 },
                    { 226, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/transport_wodny.jpg", "Transport wodny", 22 },
                    { 227, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/transport_kolejowy.jpg", "Transport kolejowy", 22 },
                    { 228, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/transport_morski.jpg", "Transport morski", 22 },
                    { 229, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/planowanie_trasy.jpg", "Planowanie trasy", 22 },
                    { 230, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/a320.jpg", "Airbus A320", 26 },
                    { 231, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/a380.jpg", "Airbus A380", 26 },
                    { 232, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/boeing_747.jpg", "Boeing 747", 26 },
                    { 233, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/boeing_777.jpg", "Boeing 777", 26 },
                    { 234, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/boeing_787.jpg", "Boeing 787", 26 },
                    { 235, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/boeing_737.jpg", "Boeing 737", 26 },
                    { 236, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/boeing_767.jpg", "Boeing 767", 26 },
                    { 237, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/a350.jpg", "Airbus A350", 26 },
                    { 238, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/reklama.jpg", "Reklama", 23 },
                    { 239, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "PR", 23 },
                    { 240, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/social_media.jpg", "Social Media", 23 },
                    { 241, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "E-mail marketing", 23 },
                    { 242, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing wizualny", 23 },
                    { 243, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing wewnętrzny", 23 },
                    { 244, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing internetowy", 23 },
                    { 245, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing_telewizyjny.jpg", "Marketing telewizyjny", 23 },
                    { 246, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing_mobilny.jpg", "Marketing mobilny", 23 },
                    { 247, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing bezpośredni", 23 },
                    { 248, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing relacji publicznych", 23 },
                    { 249, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing strategiczny", 23 },
                    { 250, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing operacyjny", 23 },
                    { 251, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing terytorialny", 23 },
                    { 252, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing usługowy", 23 },
                    { 253, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing produktowy", 23 },
                    { 254, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing segmentowy", 23 },
                    { 255, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg", "Marketing kanałowy", 23 },
                    { 256, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_sledcze.jpg", "Dziennikrastwo śledcze", 24 },
                    { 257, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_internetowe.jpg", "Dziennikarstwo internetowe", 24 },
                    { 258, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_lokalne.jpg", "Dziennikarstwo lokalne", 24 },
                    { 259, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo.jpg", "Dziennikarstwo międzynarodowe", 24 },
                    { 260, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo.jpg", "Dziennikarstwo prasowe", 24 },
                    { 261, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo.jpg", "Dziennikarstwo publicystyczne", 24 },
                    { 262, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_radiowe.jpg", "Dziennikarstwo radiowe", 24 },
                    { 263, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_telewizyjne.jpg", "Dziennikarstwo telewizyjne", 24 },
                    { 264, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo.jpg", "Dziennikarstwo wideo", 24 },
                    { 265, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_spoleczne.jpg", "Dziennikarstwo społeczne", 24 },
                    { 266, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_mieszkalna.jpg", "Architektura mieszkalna", 25 },
                    { 267, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_wnetrz.jpg", "Architektura wnętrz", 25 },
                    { 268, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_krajobrazu.jpg", "Architektura krajobrazu", 25 },
                    { 269, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_przestrzeni_publicznej.jpg", "Architektura przestrzeni publicznej", 25 },
                    { 270, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_przemyslowa.jpg", "Architektura przemysłowa", 25 },
                    { 271, null, "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_ogrodowa.jpg", "Architektura ogrodowa", 25 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "AppUserId", "RoleId", "ValidUnitl" },
                values: new object[,]
                {
                    { 1, 1, 1, null },
                    { 2, 1, 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicTitles_AcademicTitleType_Name",
                table: "AcademicTitles",
                columns: new[] { "AcademicTitleType", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ContentId",
                table: "Answers",
                column: "ContentId",
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
                name: "IX_AppUsers_Nickname",
                table: "AppUsers",
                column: "Nickname",
                unique: true);

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
                name: "IX_Contents_SearchVector",
                table: "Contents",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalRankings_AppUserId",
                table: "GlobalRankings",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagesInContent_ContentId",
                table: "ImagesInContent",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesInContent_ImageId",
                table: "ImagesInContent",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionUsageByUsers_AppUserId",
                table: "PermissionUsageByUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionUsageByUsers_PermissionId",
                table: "PermissionUsageByUsers",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PremiumPayments_IdPayment",
                table: "PremiumPayments",
                column: "IdPayment",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PremiumPayments_IdUser",
                table: "PremiumPayments",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_PremiumPayments_PaymentUrl",
                table: "PremiumPayments",
                column: "PaymentUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ContentId",
                table: "Questions",
                column: "ContentId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ParentTagId",
                table: "Tags",
                column: "ParentTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SearchVector",
                table: "Tags",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

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

            migrationBuilder.CreateIndex(
                name: "IX_TestsTags_IdTag",
                table: "TestsTags",
                column: "IdTag");

            migrationBuilder.CreateIndex(
                name: "IX_TestsTags_IdTest",
                table: "TestsTags",
                column: "IdTest");

            migrationBuilder.CreateIndex(
                name: "IX_TitleRequests_AcademicTitleId",
                table: "TitleRequests",
                column: "AcademicTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleRequests_AppUserId",
                table: "TitleRequests",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleRequests_ScanId",
                table: "TitleRequests",
                column: "ScanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_AppUserId",
                table: "UserRoles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

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
                name: "GlobalRankings");

            migrationBuilder.DropTable(
                name: "ImagesInContent");

            migrationBuilder.DropTable(
                name: "IntFunctionWrapper");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "PermissionUsageByUsers");

            migrationBuilder.DropTable(
                name: "PremiumPayments");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "TagsInQuestions");

            migrationBuilder.DropTable(
                name: "TestsQuestion");

            migrationBuilder.DropTable(
                name: "TestsTags");

            migrationBuilder.DropTable(
                name: "TitleRequests");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UsersPointsInTags");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "ReportTypes");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "AcademicTitles");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Contents");
        }
    }
}
