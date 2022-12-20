using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    public partial class AddedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PremiumPayments");

            migrationBuilder.DropColumn(
                name: "HasPremiumUntil",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "HasPremiumUntil",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PremiumPayments",
                columns: table => new
                {
                    IdPayment = table.Column<string>(type: "text", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    IdTransaction = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentUrl = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsAdmin",
                value: true);

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
    }
}
