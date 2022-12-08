using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorType.Data.Migrations
{
    public partial class UpdateAdvertisementSelectedSkillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Advertisemenets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 8, 15, 50, 48, 726, DateTimeKind.Local).AddTicks(7441));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Advertisemenets");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 8, 15, 42, 29, 834, DateTimeKind.Local).AddTicks(6272));
        }
    }
}
