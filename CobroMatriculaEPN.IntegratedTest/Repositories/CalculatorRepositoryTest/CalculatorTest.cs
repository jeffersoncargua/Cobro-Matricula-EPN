using Cobro_Matricula_EPN.Repository;
using Entity.DTO.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.IntegratedTest.Repositories.CalculatorRepositoryTest
{
    [Collection("My Collection")]
    public class CalculatorTest 
    {
        private SharedDatabaseFixture Fixture { get; }

        public CalculatorTest(SharedDatabaseFixture fixture)
        {
            Fixture = fixture;
        }


        [Theory]
        [InlineData(1, "creditos",1,true,1,2,3, "Perdida Temporal + Parcial")]
        [InlineData(1, "horas", 1, true, 1*16, 2*16, 3*16, "Perdida Temporal + Parcial")]
        [InlineData(1, "creditos", 1, true, 10, 3, 3, "Perdida Parcial")]
        [InlineData(1, "horas", 1, true, 10*16, 3*16, 3*16, "Perdida Parcial")]
        [InlineData(1, "creditos", 1, false, 10, 2, 3, "Sin Gratuidad")]
        [InlineData(1, "horas", 1, false, 10*16, 2*16, 3*16, "Sin Gratuidad")]
        //[InlineData(1, "creditos", 1, false, 0, 0, 0, "Ninguna")]
        //[InlineData(1, "creditos", 1, true, 0, 0, 0, "Ninguna")]
        [InlineData(1, "creditos", 1, true, 8, 0, 0, "Perdida Temporal")]
        [InlineData(1, "horas", 1, true, 8*16, 0*16, 0*16, "Perdida Temporal")]
        [InlineData(1, "creditos", 1, true, 28, 0, 0, "Con Gratuidad")]
        [InlineData(1, "horas", 1, true, 28* 16, 0 * 16, 0 * 16, "Con Gratuidad")]

        public async Task Calculator_WhenSendValidRequest_ReturnCalculatorResponseOk(int formationAcademy, string regimen, int quintil, bool gratuidad, int primera, int segunda, int tercera, string condicion)
        {

            using (var context = Fixture.CreateContext())
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

                var repository = new CalculatorRepository(context);

                //Act
                var response = await repository.Calculator(requestDto);

                //Assert
                Assert.True(response.Success);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(condicion, response.Calculator.Gratuidad);
            }
            

            
        }

        [Theory]
        [InlineData(1, "creditos", 1, true, 0, 0, 0, "Ninguna")]
        [InlineData(1, "horas", 1, true, 0 * 16, 0 * 16, 0 * 16, "Ninguna")]
        public async Task Calculator_WhenSendInvalidRequest_ReturnCalculatorResponseBadRequest(int formationAcademy, string regimen, int quintil, bool gratuidad, int primera, int segunda, int tercera, string condicion)
        {
            using(var context = Fixture.CreateContext())
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

                var repository = new CalculatorRepository(context);

                //Act
                var response = await repository.Calculator(requestDto);

                //Assert
                Assert.False(response.Success);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.Equal(condicion, response.Calculator.Gratuidad);
                Assert.Equal("No se ha registrado ninguna asignatura, por lo tanto no se calculará ningún valor.", response.Message);
            }
        }

        [Theory]
        [InlineData(4, "creditos", 1, true, 0, 0, 0, "No existen los parametros para realizar los calculos")]
        [InlineData(1, "horas", 1, true, 1,2,15, "Si elegiste regimen horas, debes recordar que debes ingresar en multiplos de 16. Por ejemplo: 0, 16, 32, 48,.... etc." +
                        "\n Caso contrario no se podrá realizar correctamente el calculo ")]
        public async Task Calculator_WhenParametersDoestnExistOrSendIncorrectValues_ReturnCalculatorResponseBadRequest(int formationAcademy, string regimen, int quintil, bool gratuidad, int primera, int segunda, int tercera,string message)
        {
            using(var context = Fixture.CreateContext())
            {
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

                var repository = new CalculatorRepository(context);

                //Act
                var response = await repository.Calculator(requestDto);

                //Assert
                Assert.Equal(message, response.Message);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            }
        }


    }
}
