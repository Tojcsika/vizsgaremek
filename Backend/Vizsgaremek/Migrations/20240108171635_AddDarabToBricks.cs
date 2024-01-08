using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vizsgaremek.Migrations
{
    public partial class AddDarabToBricks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Darab",
                table: "Bricks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Darab",
                table: "Bricks");
        }
    }
}
