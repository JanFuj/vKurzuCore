using Microsoft.EntityFrameworkCore.Migrations;

namespace vKurzuCore.Data.Migrations
{
    public partial class BlogTableRelatedCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Courses_CourseId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_CourseId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "RelatedCourseId",
                table: "Blogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_RelatedCourseId",
                table: "Blogs",
                column: "RelatedCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Courses_RelatedCourseId",
                table: "Blogs",
                column: "RelatedCourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Courses_RelatedCourseId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_RelatedCourseId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "RelatedCourseId",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CourseId",
                table: "Blogs",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Courses_CourseId",
                table: "Blogs",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
