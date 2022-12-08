using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorType.Data.Migrations
{
    public partial class InitialAdvertisementSelectedSkillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advertisemenets",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AdvertismenetState = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisemenets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisemenets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementSelectedSkills",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvertisementId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AdvertisementCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AdvertisemenetId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementSelectedSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementSelectedSkills_Advertisemenets_AdvertisemenetId",
                        column: x => x.AdvertisemenetId,
                        principalTable: "Advertisemenets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvertisementSelectedSkills_AdvertisementCategory_AdvertisementCategoryId",
                        column: x => x.AdvertisementCategoryId,
                        principalTable: "AdvertisementCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 8, 15, 42, 29, 834, DateTimeKind.Local).AddTicks(6272));

            migrationBuilder.CreateIndex(
                name: "IX_Advertisemenets_UserId",
                table: "Advertisemenets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementSelectedSkills_AdvertisemenetId",
                table: "AdvertisementSelectedSkills",
                column: "AdvertisemenetId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementSelectedSkills_AdvertisementCategoryId",
                table: "AdvertisementSelectedSkills",
                column: "AdvertisementCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisementSelectedSkills");

            migrationBuilder.DropTable(
                name: "Advertisemenets");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 8, 13, 0, 43, 232, DateTimeKind.Local).AddTicks(6312));
        }
    }
}
