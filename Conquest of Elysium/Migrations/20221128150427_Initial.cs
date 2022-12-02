using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conquest_of_Elysium.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
				name: "Sheets",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Health = table.Column<int>(type: "int", nullable: false),
					UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Level = table.Column<int>(type: "int", nullable: true),
					Insanity = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Sheets", x => x.Id);
				});

            migrationBuilder.CreateTable(
                name: "SheetObject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SheetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheetObject_Sheets_SheetId",
                        column: x => x.SheetId,
                        principalTable: "Sheets",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SheetObject");

            migrationBuilder.DropTable(
                name: "Sheets");
        }
    }
}
