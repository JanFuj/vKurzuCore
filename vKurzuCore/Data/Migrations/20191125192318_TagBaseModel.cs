using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vKurzuCore.Data.Migrations
{
    public partial class TagBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Tags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Tags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Tags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Tags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Tags",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Tags");
        }
    }
}
