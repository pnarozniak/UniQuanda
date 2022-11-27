using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    public partial class AddedOAuthUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:o_auth_provider_enum", "google")
                .Annotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email")
                .OldAnnotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email");

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

            migrationBuilder.CreateIndex(
                name: "IX_OAuthUsers_OAuthId_OAuthProvider",
                table: "OAuthUsers",
                columns: new[] { "OAuthId", "OAuthProvider" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OAuthUsers");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email")
                .OldAnnotation("Npgsql:Enum:o_auth_provider_enum", "google")
                .OldAnnotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email");
        }
    }
}
