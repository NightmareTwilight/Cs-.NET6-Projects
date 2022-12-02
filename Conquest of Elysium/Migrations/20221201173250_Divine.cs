using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conquest_of_Elysium.Migrations
{
    public partial class Divine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Divine",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Divine",
                table: "Sheets");
        }
    }
}
