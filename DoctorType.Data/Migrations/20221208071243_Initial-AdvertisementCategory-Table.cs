using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorType.Data.Migrations
{
    public partial class InitialAdvertisementCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertisementCategory",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UrlName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ParentId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementCategory_AdvertisementCategory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AdvertisementCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 12, 8, 10, 42, 43, 135, DateTimeKind.Local).AddTicks(1173));

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementCategory_ParentId",
                table: "AdvertisementCategory",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisementCategory");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1m,
                column: "CreateDate",
                value: new DateTime(2022, 11, 24, 11, 46, 54, 771, DateTimeKind.Local).AddTicks(3010));
        }
    }
}
