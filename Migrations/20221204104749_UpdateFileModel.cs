using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    public partial class UpdateFileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Length",
                table: "files",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "files",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "files",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "files",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "files",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "files",
                newName: "ContentType");
        }
    }
}
