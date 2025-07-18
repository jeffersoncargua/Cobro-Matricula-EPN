using Entity.DTO.BaseParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.UnitTest.DTOs.BaseParameters
{
    public class UpdatedBaseParameterTest : BaseTest
    {

        [Theory]
        [InlineData(1,"Cualquier Formacion",10000,1000,100,0.1,0.11,0.12,0.13,0.14,0.15,0.16,0.17,0.18,0.19, 0)]
        [InlineData(1, "Cualquier Formacion", 10001, 1000, 100, 1.1, 0.11, 0.12, 13, 14, 0.15, 0.16, 0.17, 0.18, 0.19, 4)]
        [InlineData(1, "Cualquier Formacion", 10000, 1001, 1000, 0.1, 0.11, 0.12, 0.13, 0.14, 0.15, 0.16, 0.17, 0.18, 0.199999999, 2)]
        public void ValidTest_UpdatedBaseParametersRequest_ReturnCorrectNumbersOfErrors(int id, string formacionAcademica, float costoOptimo, int horaPeriodoAcademico, int creditoPeriodoAcademico, float porcentajeCostoOptimoAnual, float porcentajeValorMin, float porcentajeValorMax, float porcentajeValorArancel, float porcentajePromedioAcademico, float porcentajePerdidaTemporal, float porcentajeMatriculaExtraordinario, float porcentajeMatriculaEspecial, float porcentajeRecargoSegunda, float porcentajeRecargoTercera, int errorsExpected)
        {
            // Arrange
            UpdatedBaseParameterRequestDto updatedBaseParameter = new ()
            {
                Id = id,
                FormacionAcademica = formacionAcademica,
                CostoOptimo = costoOptimo,
                HoraPeriodoAcademico = horaPeriodoAcademico,
                CreditoPeriodoAcademico = creditoPeriodoAcademico,
                PorcentajeCostoOptimoAnual = porcentajeCostoOptimoAnual,
                PorcentajeValorMin = porcentajeValorMin,
                PorcentajeValorMax = porcentajeValorMax,
                PorcentajeMatriculaEspecial = porcentajeMatriculaEspecial,
                PorcentajeValorArancel = porcentajeValorArancel,
                PorcentajePromedioAcademico = porcentajePromedioAcademico,
                PorcentajePerdidaTemporal = porcentajePerdidaTemporal,
                PorcentajeMatriculaExtraordinario = porcentajeMatriculaExtraordinario,
                PorcentajeRecargoSegunda = porcentajeRecargoSegunda,
                PorcentajeRecargoTercera = porcentajeRecargoTercera
            };

            // Act
            var validationResults = ValidateModel(updatedBaseParameter);

            // Assert
            Assert.Equal(errorsExpected, validationResults.Count);
        }
    }
}
