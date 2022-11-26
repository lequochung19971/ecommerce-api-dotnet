using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    public partial class UpdateIsDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDelete",
                table: "products",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "isDelete",
                table: "categories",
                newName: "IsDelete");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "products",
                newName: "isDelete");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "categories",
                newName: "isDelete");
        }
    }
}
