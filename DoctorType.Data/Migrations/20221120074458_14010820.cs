using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorType.Data.Migrations
{
    public partial class _14010820 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 11, 20, 11, 14, 58, 197, DateTimeKind.Local).AddTicks(8798));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 11, 20, 11, 7, 5, 974, DateTimeKind.Local).AddTicks(6142));
        }
    }
}
