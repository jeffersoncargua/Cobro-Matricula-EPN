using Entity.DTO.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.UnitTest.DTOs.Calculator
{
    public class CalculatorRequestTest : BaseTest
    {
        [Theory]
        [InlineData(1, "creditos", 1, true, 1, 5, 7, 0)]
        [InlineData(1, "horas3", 1, true, 6, -3, 7, 2)]
        [InlineData(1, "3", 8, true, 8, 5, null, 2)]
        public void CalculatorRequestDto_ReturnCorrectNumberOfErrors(int formationAcademy, string regimen, int quintil, bool gratuidad, int primera, int segunda, int tercera, int expectedErrors)
        {
            //Arrange
            CalculatorRequestDto requestDto = new()
            {
                FormacionAcademica = formationAcademy,
                Regimen = regimen,
                Quintil = quintil,
                Gratuidad = gratuidad,
                Primera = primera,
                Segunda = segunda,
                Tercera = tercera
            };

            //Act
            var validateResults = ValidateModel(requestDto);

            //Assert
            Assert.Equal(expectedErrors, validateResults.Count);
        }
    }
}
