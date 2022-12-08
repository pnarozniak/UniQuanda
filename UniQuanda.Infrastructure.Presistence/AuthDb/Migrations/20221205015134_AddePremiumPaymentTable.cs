using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    public partial class AddePremiumPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:o_auth_provider_enum", "google")
                .Annotation("Npgsql:Enum:premium_payment_status_enum", "new,pending,canceled,completed")
                .Annotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email")
                .OldAnnotation("Npgsql:Enum:o_auth_provider_enum", "google")
                .OldAnnotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email");

            migrationBuilder.AddColumn<DateTime>(
                name: "HasPremiumUntil",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

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
                        name: "FK_PremiumPayments_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PremiumPayments");

            migrationBuilder.DropColumn(
                name: "HasPremiumUntil",
                table: "Users");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:o_auth_provider_enum", "google")
                .Annotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email")
                .OldAnnotation("Npgsql:Enum:o_auth_provider_enum", "google")
                .OldAnnotation("Npgsql:Enum:premium_payment_status_enum", "new,pending,canceled,completed")
                .OldAnnotation("Npgsql:Enum:user_action_to_confirm_enum", "recover_password,new_main_email,new_extra_email");
        }
    }
}
