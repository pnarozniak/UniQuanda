using Microsoft.EntityFrameworkCore.Migrations;
using UniQuanda.Core.Domain.Enums.DbModel;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class AddedPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .Annotation("Npgsql:Enum:product_type_enum", "premium")
                .Annotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .OldAnnotation("Npgsql:Enum:report_category_enum", "user,question,answer");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductType = table.Column<ProductTypeEnum>(type: "product_type_enum", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductType);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductType", "Price" },
                values: new object[] { ProductTypeEnum.Premium, 19m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .Annotation("Npgsql:Enum:report_category_enum", "user,question,answer")
                .OldAnnotation("Npgsql:Enum:academic_title_enum", "engineer,bachelor,academic")
                .OldAnnotation("Npgsql:Enum:product_type_enum", "premium")
                .OldAnnotation("Npgsql:Enum:report_category_enum", "user,question,answer");
        }
    }
}
