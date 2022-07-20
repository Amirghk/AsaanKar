using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Infrastructure.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_FileDetails_PictureId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FileDetails_ImageId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_FileDetails_ProfilePictureId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_FileDetails_Experts_ExpertId",
                table: "FileDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileDetails",
                table: "FileDetails");

            migrationBuilder.RenameTable(
                name: "FileDetails",
                newName: "Uploads");

            migrationBuilder.RenameIndex(
                name: "IX_FileDetails_ExpertId",
                table: "Uploads",
                newName: "IX_Uploads_ExpertId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Uploads",
                table: "Uploads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Uploads_PictureId",
                table: "Categories",
                column: "PictureId",
                principalTable: "Uploads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Uploads_ImageId",
                table: "Comments",
                column: "ImageId",
                principalTable: "Uploads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Uploads_ProfilePictureId",
                table: "Customers",
                column: "ProfilePictureId",
                principalTable: "Uploads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Uploads_Experts_ExpertId",
                table: "Uploads",
                column: "ExpertId",
                principalTable: "Experts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Uploads_PictureId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Uploads_ImageId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Uploads_ProfilePictureId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Uploads_Experts_ExpertId",
                table: "Uploads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Uploads",
                table: "Uploads");

            migrationBuilder.RenameTable(
                name: "Uploads",
                newName: "FileDetails");

            migrationBuilder.RenameIndex(
                name: "IX_Uploads_ExpertId",
                table: "FileDetails",
                newName: "IX_FileDetails_ExpertId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileDetails",
                table: "FileDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_FileDetails_PictureId",
                table: "Categories",
                column: "PictureId",
                principalTable: "FileDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FileDetails_ImageId",
                table: "Comments",
                column: "ImageId",
                principalTable: "FileDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_FileDetails_ProfilePictureId",
                table: "Customers",
                column: "ProfilePictureId",
                principalTable: "FileDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileDetails_Experts_ExpertId",
                table: "FileDetails",
                column: "ExpertId",
                principalTable: "Experts",
                principalColumn: "Id");
        }
    }
}
