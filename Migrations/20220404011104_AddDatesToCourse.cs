using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Village22.Migrations
{
    public partial class AddDatesToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "Courses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "Courses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "Courses");
        }
    }
}
