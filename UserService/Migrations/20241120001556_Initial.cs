using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "566ac1e7-43a0-46d0-92eb-d2a2755c9ab9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b9140e1-ef1f-4aeb-8ea1-e1bc94a009da");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "112e8fe8-5c03-4cc4-b3ca-1de83c24aa25", null, "Admin", "ADMIN" },
                    { "640938f1-d2f4-47ad-b642-733f06fe899e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "112e8fe8-5c03-4cc4-b3ca-1de83c24aa25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "640938f1-d2f4-47ad-b642-733f06fe899e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "566ac1e7-43a0-46d0-92eb-d2a2755c9ab9", null, "User", "USER" },
                    { "5b9140e1-ef1f-4aeb-8ea1-e1bc94a009da", null, "Admin", "ADMIN" }
                });
        }
    }
}
