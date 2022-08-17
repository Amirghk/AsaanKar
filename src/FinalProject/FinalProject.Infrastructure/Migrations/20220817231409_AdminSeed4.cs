using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Infrastructure.Migrations
{
    public partial class AdminSeed4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d74ddd24-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "179b9af7-469d-4a48-8b48-dff554713e33", "ADMINSS@GMAIL.COM", "AQAAAAEAACcQAAAAENwtMJDzo4LuKWZlLRFvU+YezqnSBfY5r13h3nBJCqNYcKjHcbXHVcZTZ3I1F4/05g==", "bab349a0-7c45-4ced-b42f-ae796c3805eb", "adminss@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d74ddd24-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "180b428e-62bc-4012-a509-da4c6b5e879a", "ADMINS", "AQAAAAEAACcQAAAAEL3tdmqJquRqHr1OutA5cc6gCfxbL3ezZtalEsQltMMByOSARYkll7U1qjS5AH0dow==", "c2933967-15ba-4f8e-b8aa-fe5c852d3c22", "Admins" });
        }
    }
}
