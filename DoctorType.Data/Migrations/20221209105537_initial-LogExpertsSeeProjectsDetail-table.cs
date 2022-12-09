using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorType.Data.Migrations
{
    public partial class initialLogExpertsSeeProjectsDetailtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogExpertsSeeProjectsDetail",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvertisementId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogExpertsSeeProjectsDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogExpertsSeeProjectsDetail_Advertisemenets_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisemenets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogExpertsSeeProjectsDetail_Users_UserId",
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
                value: new DateTime(2022, 12, 9, 14, 25, 36, 964, DateTimeKind.Local).AddTicks(9693));

            migrationBuilder.CreateIndex(
                name: "IX_LogExpertsSeeProjectsDetail_AdvertisementId",
                table: "LogExpertsSeeProjectsDetail",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_LogExpertsSeeProjectsDetail_UserId",
                table: "LogExpertsSeeProjectsDetail",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogExpertsSeeProjectsDetail");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 8, 18, 1, 32, 139, DateTimeKind.Local).AddTicks(9876));
        }
    }
}
