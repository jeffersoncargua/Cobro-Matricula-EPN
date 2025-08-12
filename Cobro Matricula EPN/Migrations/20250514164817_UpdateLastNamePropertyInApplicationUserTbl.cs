// <copyright file="20250514164817_UpdateLastNamePropertyInApplicationUserTbl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cobro_Matricula_EPN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLastNamePropertyInApplicationUserTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9782770b-8703-45d6-a1c4-4e6256ec3b72");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98b46bf0-6dbd-414d-b760-11df317e1e94");

            migrationBuilder.RenameColumn(
                name: "LasName",
                table: "AspNetUsers",
                newName: "LastName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "LasName");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9782770b-8703-45d6-a1c4-4e6256ec3b72", "2", "Assistant", "ASSISTANT" },
                    { "98b46bf0-6dbd-414d-b760-11df317e1e94", "1", "Admin", "ADMIN" },
                });
        }
    }
}
