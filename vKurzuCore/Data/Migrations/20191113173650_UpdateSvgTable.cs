using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vKurzuCore.Data.Migrations
{
    public partial class UpdateSvgTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Svgs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Svgs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Svgs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Svgs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Svgs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Svgs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Svgs");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Svgs");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Svgs");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Svgs");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Svgs");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Svgs");
        }
    }
}
