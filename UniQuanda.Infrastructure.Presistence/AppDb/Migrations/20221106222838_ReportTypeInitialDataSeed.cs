using Microsoft.EntityFrameworkCore.Migrations;
using UniQuanda.Core.Domain.Enums;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class ReportTypeInitialDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
