using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conquest_of_Elysium.Migrations
{
    public partial class Equipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chest",
                table: "Sheets",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Head",
                table: "Sheets",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LArm",
                table: "Sheets",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LLeg",
                table: "Sheets",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RArm",
                table: "Sheets",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RLeg",
                table: "Sheets",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chest",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Head",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "LArm",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "LLeg",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "RArm",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "RLeg",
                table: "Sheets");
        }
    }
}
