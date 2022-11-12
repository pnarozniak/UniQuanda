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

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION checkAddEmailAction() RETURNS TRIGGER AS $emailActionCheck$ BEGIN IF NEW.\"ActionType\" = 2 OR NEW.\"ActionType\" = 3 THEN IF EXISTS (SELECT * FROM \"UsersActionsToConfirm\" WHERE (\"IdUser\" = NEW.\"IdUser\" AND (\"ActionType\" = 2 OR \"ActionType\" = 3))) THEN RETURN NULL; ELSE RETURN NEW; END IF; ELSE RETURN NEW; END IF; END; $emailActionCheck$ LANGUAGE plpgsql;" +
                "CREATE TRIGGER add_email_action BEFORE INSERT OR UPDATE ON \"UsersActionsToConfirm\" FOR EACH ROW EXECUTE PROCEDURE checkAddEmailAction();");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersActionsToConfirm_UsersEmails_IdUserEmail",
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

            migrationBuilder.Sql("DROP TRIGGER add_email_action ON transactions;DROP FUNCTION checkAddEmailAction();");
        }
    }
}
