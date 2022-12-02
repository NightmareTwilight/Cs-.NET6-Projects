using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conquest_of_Elysium.Migrations
{
    public partial class Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_AspNetUsers_UserId",
                table: "Sheets");

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_AspNetUsers_UserId",
                table: "Sheets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_AspNetUsers_UserId",
                table: "Sheets");

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_AspNetUsers_UserId",
                table: "Sheets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
