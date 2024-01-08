using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Migrations.Identity
{
    public partial class InsertingRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23ed7650-e083-452e-a28d-f50bc879cd91", "a7161598-8baa-49fe-9abd-a1675b754b5e", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3549fc88-2b8a-4ec7-b710-2b4fbc064f4e", "032a9bf6-b7a9-448f-9666-addee00b2bd1", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23ed7650-e083-452e-a28d-f50bc879cd91");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3549fc88-2b8a-4ec7-b710-2b4fbc064f4e");
        }
    }
}
