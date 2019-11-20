using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vKurzuCore.Data.Migrations
{
    public partial class AdminNoteBaseModelImageFileBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "ImageFiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "ImageFiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ImageFiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ImageFiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "ImageFiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "ImageFiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "AdminNotes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "AdminNotes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "AdminNotes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AdminNotes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "AdminNotes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "AdminNotes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "AdminNotes");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "AdminNotes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "AdminNotes");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AdminNotes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "AdminNotes");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "AdminNotes");
        }
    }
}
