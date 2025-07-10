using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cobro_Matricula_EPN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBaseParameterTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f494a80-cde2-448a-9c17-ca56a1c6e176");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1d4bf5e-7373-47f6-8831-6f138754ae04");

            migrationBuilder.RenameColumn(
                name: "PorcentajeArancelEspecial",
                table: "BaseParameters",
                newName: "PorcentajeMatriculaEspecial");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13290df7-b8c1-4f7b-b8cb-0d309a71483a", "2", "Assistant", "ASSISTANT" },
                    { "1467b441-83ae-4f05-a65a-3eef43be0445", "1", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13290df7-b8c1-4f7b-b8cb-0d309a71483a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1467b441-83ae-4f05-a65a-3eef43be0445");

            migrationBuilder.RenameColumn(
                name: "PorcentajeMatriculaEspecial",
                table: "BaseParameters",
                newName: "PorcentajeArancelEspecial");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f494a80-cde2-448a-9c17-ca56a1c6e176", "1", "Admin", "ADMIN" },
                    { "a1d4bf5e-7373-47f6-8831-6f138754ae04", "2", "Assistant", "ASSISTANT" }
                });
        }
    }
}
