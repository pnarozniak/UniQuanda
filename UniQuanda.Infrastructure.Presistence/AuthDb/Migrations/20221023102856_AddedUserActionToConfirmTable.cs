using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    public partial class AddedUserActionToConfirmTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password");

            migrationBuilder.CreateTable(
                name: "UsersActionsToConfirm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersActionsToConfirm_ActionType_IdUser",
                table: "UsersActionsToConfirm",
                columns: new[] { "ActionType", "IdUser" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersActionsToConfirm_IdUser",
                table: "UsersActionsToConfirm",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersActionsToConfirm");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password");
        }
    }
}
