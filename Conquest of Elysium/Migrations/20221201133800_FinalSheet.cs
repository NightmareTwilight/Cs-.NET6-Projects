using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conquest_of_Elysium.Migrations
{
    public partial class FinalSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bless",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effects",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Items",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Passive",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Bless",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Effects",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Items",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Passive",
                table: "Sheets");
        }
    }
}
