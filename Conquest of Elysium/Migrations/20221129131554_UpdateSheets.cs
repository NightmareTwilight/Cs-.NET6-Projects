using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conquest_of_Elysium.Migrations
{
    public partial class UpdateSheets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Sheets",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Sheets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Sheets_UserId",
                table: "Sheets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_AspNetUsers_UserId",
                table: "Sheets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_AspNetUsers_UserId",
                table: "Sheets");

            migrationBuilder.DropIndex(
                name: "IX_Sheets_UserId",
                table: "Sheets");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sheets",
                newName: "UserID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Sheets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
