using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    public partial class EngineeringDiploma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:o_auth_provider_enum", "google")
                .Annotation("Npgsql:Enum:premium_payment_status_enum", "new,pending,canceled,completed")
                .Annotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nickname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    HashedPassword = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OAuthUsers",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    OAuthId = table.Column<string>(type: "text", nullable: false),
                    OAuthProvider = table.Column<int>(type: "integer", nullable: false),
                    OAuthRegisterConfirmationCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuthUsers", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_OAuthUsers_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempUsers",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    EmailConfirmationCode = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    ExistsUntil = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: true),
                    LastName = table.Column<string>(type: "character varying(51)", maxLength: 51, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Contact = table.Column<string>(type: "character varying(22)", maxLength: 22, nullable: true),
                    City = table.Column<string>(type: "character varying(57)", maxLength: 57, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUsers", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_TempUsers_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersEmails_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersActionsToConfirm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    IdUserEmail = table.Column<int>(type: "integer", nullable: true),
                    ConfirmationToken = table.Column<string>(type: "text", nullable: false),
                    ExistsUntil = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ActionType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersActionsToConfirm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersActionsToConfirm_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersActionsToConfirm_UsersEmails_IdUserEmail",
                        column: x => x.IdUserEmail,
                        principalTable: "UsersEmails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "Nickname", "RefreshToken", "RefreshTokenExp" },
                values: new object[] { 1, "$2a$12$bIkUNGSkHjgVl80kICadyezV4AgRo6oMwuIEC3X9ian.d7a6xJRIe", "Programista", null, null });

            migrationBuilder.InsertData(
                table: "UsersEmails",
                columns: new[] { "Id", "IdUser", "IsMain", "Value" },
                values: new object[] { 1, 1, true, "user@uniquanda.pl" });

            migrationBuilder.CreateIndex(
                name: "IX_OAuthUsers_OAuthId_OAuthProvider",
                table: "OAuthUsers",
                columns: new[] { "OAuthId", "OAuthProvider" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Nickname",
                table: "Users",
                column: "Nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersActionsToConfirm_ActionType_IdUser",
                table: "UsersActionsToConfirm",
                columns: new[] { "ActionType", "IdUser" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersActionsToConfirm_IdUser",
                table: "UsersActionsToConfirm",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UsersActionsToConfirm_IdUserEmail",
                table: "UsersActionsToConfirm",
                column: "IdUserEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersEmails_IdUser",
                table: "UsersEmails",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UsersEmails_Value",
                table: "UsersEmails",
                column: "Value",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OAuthUsers");

            migrationBuilder.DropTable(
                name: "TempUsers");

            migrationBuilder.DropTable(
                name: "UsersActionsToConfirm");

            migrationBuilder.DropTable(
                name: "UsersEmails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
