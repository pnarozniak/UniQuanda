using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    public partial class AddedConfirmationForUserEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email")
                .OldAnnotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password");

            migrationBuilder.AddColumn<int>(
                name: "IdUserEmail",
                table: "UsersActionsToConfirm",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersActionsToConfirm_ActionType_IdUserEmail_IdUser",
                table: "UsersActionsToConfirm",
                columns: new[] { "ActionType", "IdUserEmail", "IdUser" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersActionsToConfirm_IdUserEmail",
                table: "UsersActionsToConfirm",
                column: "IdUserEmail",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersActionsToConfirm_UsersEmails_IdUserEmail",
                table: "UsersActionsToConfirm",
                column: "IdUserEmail",
                principalTable: "UsersEmails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersActionsToConfirm_UsersEmails_IdUserEmail",
                table: "UsersActionsToConfirm");

            migrationBuilder.DropIndex(
                name: "IX_UsersActionsToConfirm_ActionType_IdUserEmail_IdUser",
                table: "UsersActionsToConfirm");

            migrationBuilder.DropIndex(
                name: "IX_UsersActionsToConfirm_IdUserEmail",
                table: "UsersActionsToConfirm");

            migrationBuilder.DropColumn(
                name: "IdUserEmail",
                table: "UsersActionsToConfirm");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password")
                .OldAnnotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email");
        }
    }
}
