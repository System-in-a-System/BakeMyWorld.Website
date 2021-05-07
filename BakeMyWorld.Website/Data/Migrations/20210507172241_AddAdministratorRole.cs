using Microsoft.EntityFrameworkCore.Migrations;

namespace BakeMyWorld.Website.Migrations
{
    public partial class AddAdministratorRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29b27620-2844-4ea0-9001-aa5759f926e3", "b402fb88-6e7f-4229-823a-b83e81041126", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29b27620-2844-4ea0-9001-aa5759f926e3");
        }
    }
}
