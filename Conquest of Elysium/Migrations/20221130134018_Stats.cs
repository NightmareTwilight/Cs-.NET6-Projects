using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conquest_of_Elysium.Migrations
{
    public partial class Stats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AWE",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DEX",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiI",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "INT",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaI",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaR",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeR",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PyR",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RaS",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STR",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpC",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TNK",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeS",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AWE",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "DEX",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "DiI",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "INT",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "MaI",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "MaR",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "MeR",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "PyR",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "RaS",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "STR",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "SpC",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "TNK",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "WeS",
                table: "Sheets");
        }
    }
}
