using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Infrastructure.Migrations
{
    public partial class CommentFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Uploads_ImageId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ImageId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ImageId",
                table: "Comments",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Uploads_ImageId",
                table: "Comments",
                column: "ImageId",
                principalTable: "Uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Uploads_ImageId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ImageId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ImageId",
                table: "Comments",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Uploads_ImageId",
                table: "Comments",
                column: "ImageId",
                principalTable: "Uploads",
                principalColumn: "Id");
        }
    }
}
