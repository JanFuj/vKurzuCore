using Microsoft.EntityFrameworkCore.Migrations;

namespace vKurzuCore.Data.Migrations
{
    public partial class UniqueIndexSvgName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Svgs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Svgs_Name",
                table: "Svgs",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Svgs_Name",
                table: "Svgs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Svgs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
