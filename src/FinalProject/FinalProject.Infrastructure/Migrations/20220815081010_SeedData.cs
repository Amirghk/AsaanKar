using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Infrastructure.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "IsDeleted", "IsSupported", "Name" },
                values: new object[] { 1, false, true, "Tehran" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "IsDeleted", "IsSupported", "Name", "ProvinceId" },
                values: new object[] { 1, false, true, "Tehran", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
