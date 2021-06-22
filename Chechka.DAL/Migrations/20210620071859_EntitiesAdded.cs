using Microsoft.EntityFrameworkCore.Migrations;

namespace Chechka.DAL.Migrations
{
    public partial class EntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComputerPartGroups",
                columns: table => new
                {
                    ComputerPartGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerPartGroups", x => x.ComputerPartGroupId);
                });

            migrationBuilder.CreateTable(
                name: "ComputerParts",
                columns: table => new
                {
                    ComputerPartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerPartName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComputerPartGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerParts", x => x.ComputerPartId);
                    table.ForeignKey(
                        name: "FK_ComputerParts_ComputerPartGroups_ComputerPartGroupId",
                        column: x => x.ComputerPartGroupId,
                        principalTable: "ComputerPartGroups",
                        principalColumn: "ComputerPartGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerParts_ComputerPartGroupId",
                table: "ComputerParts",
                column: "ComputerPartGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputerParts");

            migrationBuilder.DropTable(
                name: "ComputerPartGroups");
        }
    }
}
