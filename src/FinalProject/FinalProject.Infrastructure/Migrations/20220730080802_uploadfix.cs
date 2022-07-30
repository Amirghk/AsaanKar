using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Infrastructure.Migrations
{
    public partial class uploadfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Uploads_ProfilePictureId",
                table: "Customers");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Uploads_ProfilePictureId",
                table: "Customers",
                column: "ProfilePictureId",
                principalTable: "Uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Uploads_ProfilePictureId",
                table: "Customers");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Uploads_ProfilePictureId",
                table: "Customers",
                column: "ProfilePictureId",
                principalTable: "Uploads",
                principalColumn: "Id");
        }
    }
}
