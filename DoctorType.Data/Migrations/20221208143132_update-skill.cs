using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorType.Data.Migrations
{
    public partial class updateskill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementSelectedSkills_Advertisemenets_AdvertisemenetId",
                table: "AdvertisementSelectedSkills");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementSelectedSkills_AdvertisemenetId",
                table: "AdvertisementSelectedSkills");

            migrationBuilder.DropColumn(
                name: "AdvertisemenetId",
                table: "AdvertisementSelectedSkills");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 8, 18, 1, 32, 139, DateTimeKind.Local).AddTicks(9876));

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementSelectedSkills_AdvertisementId",
                table: "AdvertisementSelectedSkills",
                column: "AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementSelectedSkills_Advertisemenets_AdvertisementId",
                table: "AdvertisementSelectedSkills",
                column: "AdvertisementId",
                principalTable: "Advertisemenets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementSelectedSkills_Advertisemenets_AdvertisementId",
                table: "AdvertisementSelectedSkills");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementSelectedSkills_AdvertisementId",
                table: "AdvertisementSelectedSkills");

            migrationBuilder.AddColumn<decimal>(
                name: "AdvertisemenetId",
                table: "AdvertisementSelectedSkills",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 8, 15, 50, 48, 726, DateTimeKind.Local).AddTicks(7441));

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementSelectedSkills_AdvertisemenetId",
                table: "AdvertisementSelectedSkills",
                column: "AdvertisemenetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementSelectedSkills_Advertisemenets_AdvertisemenetId",
                table: "AdvertisementSelectedSkills",
                column: "AdvertisemenetId",
                principalTable: "Advertisemenets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
