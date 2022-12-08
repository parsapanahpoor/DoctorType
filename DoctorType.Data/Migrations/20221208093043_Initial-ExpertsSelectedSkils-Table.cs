using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorType.Data.Migrations
{
    public partial class InitialExpertsSelectedSkilsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpertsSelectedSkils",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvertisementCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertsSelectedSkils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertsSelectedSkils_AdvertisementCategory_AdvertisementCategoryId",
                        column: x => x.AdvertisementCategoryId,
                        principalTable: "AdvertisementCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpertsSelectedSkils_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 8, 13, 0, 43, 232, DateTimeKind.Local).AddTicks(6312));

            migrationBuilder.CreateIndex(
                name: "IX_ExpertsSelectedSkils_AdvertisementCategoryId",
                table: "ExpertsSelectedSkils",
                column: "AdvertisementCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertsSelectedSkils_UserId",
                table: "ExpertsSelectedSkils",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertsSelectedSkils");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 8, 12, 8, 44, 5, DateTimeKind.Local).AddTicks(809));
        }
    }
}
