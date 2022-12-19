using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class AddedUserScans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScanId",
                table: "TitleRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TitleRequests_ScanId",
                table: "TitleRequests",
                column: "ScanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleRequests_Images_ScanId",
                table: "TitleRequests",
                column: "ScanId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TitleRequests_Images_ScanId",
                table: "TitleRequests");

            migrationBuilder.DropIndex(
                name: "IX_TitleRequests_ScanId",
                table: "TitleRequests");

            migrationBuilder.DropColumn(
                name: "ScanId",
                table: "TitleRequests");
        }
    }
}
