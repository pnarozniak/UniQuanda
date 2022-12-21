using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class RemovedUnusedSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "solve-course");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AllowedUsages", "LimitRefreshPeriod", "PermissionId", "RoleId" },
                values: new object[] { 3, 604800, 1, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AllowedUsages", "LimitRefreshPeriod", "RoleId" },
                values: new object[] { null, null, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AllowedUsages", "LimitRefreshPeriod", "PermissionId", "RoleId" },
                values: new object[] { 5, 604800, 1, 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AllowedUsages", "LimitRefreshPeriod" },
                values: new object[] { 3, 604800 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "solve-automatic-test");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "solve-course" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AllowedUsages", "LimitRefreshPeriod", "PermissionId", "RoleId" },
                values: new object[] { null, null, 3, 1 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AllowedUsages", "LimitRefreshPeriod", "RoleId" },
                values: new object[] { 3, 604800, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AllowedUsages", "LimitRefreshPeriod", "PermissionId", "RoleId" },
                values: new object[] { 1, 86400, 3, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AllowedUsages", "LimitRefreshPeriod" },
                values: new object[] { null, null });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "AllowedUsages", "LimitRefreshPeriod", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 6, null, null, 3, 3 },
                    { 7, 5, 604800, 1, 4 },
                    { 8, null, null, 3, 4 },
                    { 9, 3, 604800, 1, 5 },
                    { 10, 1, 86400, 3, 5 }
                });
        }
    }
}
