using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Infrastructure.Migrations
{
    public partial class AdminSeed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd24-6340-4840-95c2-db12554843e5");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d74ddd24-6340-4840-95c2-db12554843e5", 0, "180b428e-62bc-4012-a509-da4c6b5e879a", "adminss@gmail.com", false, false, null, "ADMINSS@GMAIL.COM", "ADMINS", "AQAAAAEAACcQAAAAEL3tdmqJquRqHr1OutA5cc6gCfxbL3ezZtalEsQltMMByOSARYkll7U1qjS5AH0dow==", "1234567890", false, "c2933967-15ba-4f8e-b8aa-fe5c852d3c22", false, "Admins" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 52, "IsAdmin", "True", "d74ddd24-6340-4840-95c2-db12554843e5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d74ddd24-6340-4840-95c2-db12554843e5");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd24-6340-4840-95c2-db12554843e5", 0, "c111800a-d98a-4cb6-9fd8-b9d7504983de", "admins@gmail.com", false, false, null, "ADMINS@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEEmLXbgeB8HceTHw4D5Ne3enzg9sXl8VYVsWm4ojhDz4UnQ/Ytz7+ZAc592/+pSMzw==", "1234567890", false, "aee4478a-ca7a-4664-a381-dd5a657686ba", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 51, "IsAdmin", "True", "b74ddd24-6340-4840-95c2-db12554843e5" });
        }
    }
}
