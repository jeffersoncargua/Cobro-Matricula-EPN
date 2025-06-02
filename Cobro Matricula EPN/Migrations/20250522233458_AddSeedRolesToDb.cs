using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cobro_Matricula_EPN.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "893bd138-ead2-471f-817d-507b73a752f6", "1", "Admin", "ADMIN" },
                    { "c9bbc2e4-4c91-4383-9a44-fb10231cdbe2", "2", "Assistant", "ASSISTANT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "893bd138-ead2-471f-817d-507b73a752f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9bbc2e4-4c91-4383-9a44-fb10231cdbe2");
        }
    }
}
