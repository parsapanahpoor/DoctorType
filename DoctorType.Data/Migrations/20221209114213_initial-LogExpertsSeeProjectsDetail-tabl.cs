using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorType.Data.Migrations
{
    public partial class initialLogExpertsSeeProjectsDetailtabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "Tariffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 9, 15, 12, 12, 540, DateTimeKind.Local).AddTicks(2635));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "Tariffs");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 9, 14, 25, 36, 964, DateTimeKind.Local).AddTicks(9693));
        }
    }
}
