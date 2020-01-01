using Microsoft.EntityFrameworkCore.Migrations;

namespace vKurzuCore.Data.Migrations
{
    public partial class UrlTitleUniqueTutorialPostAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlTitle",
                table: "TutorialPosts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UrlTitle",
                table: "TutorialCategories",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TutorialPosts_UrlTitle",
                table: "TutorialPosts",
                column: "UrlTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TutorialCategories_UrlTitle",
                table: "TutorialCategories",
                column: "UrlTitle",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TutorialPosts_UrlTitle",
                table: "TutorialPosts");

            migrationBuilder.DropIndex(
                name: "IX_TutorialCategories_UrlTitle",
                table: "TutorialCategories");

            migrationBuilder.AlterColumn<string>(
                name: "UrlTitle",
                table: "TutorialPosts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UrlTitle",
                table: "TutorialCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
