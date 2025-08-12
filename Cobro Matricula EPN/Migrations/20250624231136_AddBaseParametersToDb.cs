// <copyright file="20250624231136_AddBaseParametersToDb.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cobro_Matricula_EPN.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseParametersToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "893bd138-ead2-471f-817d-507b73a752f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9bbc2e4-4c91-4383-9a44-fb10231cdbe2");

            migrationBuilder.CreateTable(
                name: "BaseParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormacionAcademica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoOptimo = table.Column<float>(type: "real", nullable: false),
                    CostoOptimoPeriodo = table.Column<float>(type: "real", nullable: false),
                    ValorMin = table.Column<float>(type: "real", nullable: false),
                    ValorMatriculaMin = table.Column<float>(type: "real", nullable: false),
                    ValorArancelMin = table.Column<float>(type: "real", nullable: false),
                    ValorMax = table.Column<float>(type: "real", nullable: false),
                    ValorMatriculaMax = table.Column<float>(type: "real", nullable: false),
                    ValorArancelMax = table.Column<float>(type: "real", nullable: false),
                    HoraPeriodoAcademico = table.Column<int>(type: "int", nullable: false),
                    HoraPromedioPeriodoAcademico = table.Column<float>(type: "real", nullable: false),
                    CreditoPeriodoAcademico = table.Column<int>(type: "int", nullable: false),
                    CreditoPerdidaTemporal = table.Column<float>(type: "real", nullable: false),
                    CostoHoraPeriodo = table.Column<float>(type: "real", nullable: false),
                    PorcentajeCostoOptimoAnual = table.Column<float>(type: "real", nullable: false),
                    PorcentajeValorMin = table.Column<float>(type: "real", nullable: false),
                    PorcentajeValorMax = table.Column<float>(type: "real", nullable: false),
                    PorcentajeValorArancel = table.Column<float>(type: "real", nullable: false),
                    PorcentajePromedioAcademico = table.Column<float>(type: "real", nullable: false),
                    PorcentajePerdidaTemporal = table.Column<float>(type: "real", nullable: false),
                    PorcentajeMatriculaExtraordinario = table.Column<float>(type: "real", nullable: false),
                    PorcentajeArancelEspecial = table.Column<float>(type: "real", nullable: false),
                    PorcentajeRecargoSegunda = table.Column<float>(type: "real", nullable: false),
                    PorcentajeRecargoTercera = table.Column<float>(type: "real", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseParameters", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BaseParameters",
                columns: new[] { "Id", "CostoHoraPeriodo", "CostoOptimo", "CostoOptimoPeriodo", "CreditoPerdidaTemporal", "CreditoPeriodoAcademico", "FormacionAcademica", "HoraPeriodoAcademico", "HoraPromedioPeriodoAcademico", "PorcentajeArancelEspecial", "PorcentajeCostoOptimoAnual", "PorcentajeMatriculaExtraordinario", "PorcentajePerdidaTemporal", "PorcentajePromedioAcademico", "PorcentajeRecargoSegunda", "PorcentajeRecargoTercera", "PorcentajeValorArancel", "PorcentajeValorMax", "PorcentajeValorMin", "ValorArancelMax", "ValorArancelMin", "ValorMatriculaMax", "ValorMatriculaMin", "ValorMax", "ValorMin" },
                values: new object[,]
                {
                    { 1, 4.0367618f, 3325f, 1662.5f, 9f, 15, "Ingeniería", 720, 374.4f, 0.25f, 0.1f, 0.25f, 0.6f, 0.52f, 0.1f, 0.21f, 0.1f, 0.5f, 0.1f, 755.6818f, 151.13637f, 75.568184f, 15.113637f, 831.25f, 166.25f },
                    { 2, 4.0367618f, 3325f, 1662.5f, 5f, 15, "Tecnología", 720, 374.4f, 0.25f, 0.1f, 0.25f, 0.6f, 0.56f, 0.1f, 0.21f, 0.1f, 0.5f, 0.1f, 755.6818f, 151.13637f, 75.568184f, 15.113637f, 831.25f, 166.25f },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseParameters");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "893bd138-ead2-471f-817d-507b73a752f6", "1", "Admin", "ADMIN" },
                    { "c9bbc2e4-4c91-4383-9a44-fb10231cdbe2", "2", "Assistant", "ASSISTANT" },
                });
        }
    }
}
