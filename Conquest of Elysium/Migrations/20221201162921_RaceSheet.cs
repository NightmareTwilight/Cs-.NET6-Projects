using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conquest_of_Elysium.Migrations
{
    public partial class RaceSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "Sheets",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Race",
                table: "Sheets");
        }
    }
}
