using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vKurzuCore.Data.Migrations
{
    public partial class TutorialCategoryTutorialPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TutorialCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Changed = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UrlTitle = table.Column<string>(nullable: false),
                    HeaderImage = table.Column<string>(nullable: true),
                    SocialSharingImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TutorialPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Changed = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    HeaderImage = table.Column<string>(nullable: true),
                    SocialSharingImage = table.Column<string>(nullable: true),
                    UrlTitle = table.Column<string>(nullable: false),
                    RelatedCourseId = table.Column<int>(nullable: false),
                    TutorialCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorialPosts_TutorialCategories_TutorialCategoryId",
                        column: x => x.TutorialCategoryId,
                        principalTable: "TutorialCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorialPostTag",
                columns: table => new
                {
                    TutorialPostId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialPostTag", x => new { x.TutorialPostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TutorialPostTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorialPostTag_TutorialPosts_TutorialPostId",
                        column: x => x.TutorialPostId,
                        principalTable: "TutorialPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorialPosts_TutorialCategoryId",
                table: "TutorialPosts",
                column: "TutorialCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorialPostTag_TagId",
                table: "TutorialPostTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorialPostTag");

            migrationBuilder.DropTable(
                name: "TutorialPosts");

            migrationBuilder.DropTable(
                name: "TutorialCategories");
        }
    }
}
