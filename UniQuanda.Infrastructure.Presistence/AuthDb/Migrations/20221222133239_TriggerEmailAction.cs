using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    public partial class TriggerEmailAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION checkAddEmailAction() RETURNS TRIGGER AS $emailActionCheck$ BEGIN IF NEW.\"ActionType\" = 2 OR NEW.\"ActionType\" = 3 THEN IF EXISTS (SELECT * FROM \"UsersActionsToConfirm\" WHERE (\"IdUser\" = NEW.\"IdUser\" AND (\"ActionType\" = 2 OR \"ActionType\" = 3))) THEN  IF NEW.\"ConfirmationToken\" != OLD.\"ConfirmationToken\" AND NEW.\"ExistsUntil\" != OLD.\"ExistsUntil\" THEN RETURN NEW; END IF; RETURN NULL; ELSE RETURN NEW; END IF; ELSE RETURN NEW; END IF; END; $emailActionCheck$ LANGUAGE plpgsql;CREATE TRIGGER add_email_action BEFORE INSERT OR UPDATE ON \"UsersActionsToConfirm\" FOR EACH ROW EXECUTE PROCEDURE checkAddEmailAction();");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER add_email_action ON transactions;DROP FUNCTION checkAddEmailAction();");
        }
    }
}
