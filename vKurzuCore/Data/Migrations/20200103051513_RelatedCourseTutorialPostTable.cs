using Microsoft.EntityFrameworkCore.Migrations;

namespace vKurzuCore.Data.Migrations
{
    public partial class RelatedCourseTutorialPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TutorialPosts_RelatedCourseId",
                table: "TutorialPosts",
                column: "RelatedCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorialPosts_Courses_RelatedCourseId",
                table: "TutorialPosts",
                column: "RelatedCourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorialPosts_Courses_RelatedCourseId",
                table: "TutorialPosts");

            migrationBuilder.DropIndex(
                name: "IX_TutorialPosts_RelatedCourseId",
                table: "TutorialPosts");
        }
    }
}
