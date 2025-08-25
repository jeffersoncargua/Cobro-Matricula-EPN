using Entity.DTO.Calculator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace CobroMatriculaEPN.FunctionalTest.Controller.CalculatorControllerTest
{

    public class CalculatorControllerTest : BaseControllerTest
    {
        public CalculatorControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData(1, "creditos", 1, false, 1, 1, 1, "Sin Gratuidad", "El cálculo se realizo correctamente.")]
        [InlineData(1, "horas", 1, false, 1 * 16, 1 * 16, 1 * 16, "Sin Gratuidad", "El cálculo se realizo correctamente.")]
        [InlineData(1, "creditos", 1, true, 1, 1, 1, "Perdida Temporal + Parcial", "El cálculo se realizo correctamente.")]
        [InlineData(1, "horas", 1, true, 1 * 16, 1 * 16, 1 * 16, "Perdida Temporal + Parcial", "El cálculo se realizo correctamente.")]
        [InlineData(1, "creditos", 1, true, 10, 1, 1, "Perdida Parcial", "El cálculo se realizo correctamente.")]
        [InlineData(1, "horas", 1, true, 10 * 16, 1 * 16, 1 * 16, "Perdida Parcial", "El cálculo se realizo correctamente.")]
        [InlineData(1, "creditos", 1, true, 8, 0, 0, "Perdida Temporal", "El cálculo se realizo correctamente.")]
        [InlineData(1, "horas", 1, true, 8 * 16, 0 * 16, 0 * 16, "Perdida Temporal", "El cálculo se realizo correctamente.")]
        [InlineData(1, "creditos", 1, true, 10, 0, 0, "Con Gratuidad", "El cálculo se realizo correctamente.")]
        [InlineData(1, "horas", 1, true, 10 * 16, 0 * 16, 0 * 16, "Con Gratuidad", "El cálculo se realizo correctamente.")]
        //[InlineData(1, "creditos", 1, true, 0, 0, 0, "Ninguna", "No se ha registrado ninguna asignatura, por lo tanto no se calculará ningún valor.")]
        //[InlineData(1, "horas", 1, true, 0 * 16, 0 * 16, 0 * 16, "Ninguna", "No se ha registrado ninguna asignatura, por lo tanto no se calculará ningún valor.")]
        public async Task CalculatorPay_WhenSendValidRequest_ReturnStatusCodeOk(int formationAcademy, string regimen, int quintil, bool gratuidad, int primera, int segunda, int tercera, string condicion, string message)
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

            var stringContent = new StringContent(JsonConvert.SerializeObject(requestDto), Encoding.UTF8, "application/json");

            var client = this.GetNewClient();

            //Act
            var response = await client.PostAsync("api/Calculator/CalculatorPay", stringContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var calculatorResponse = JsonConvert.DeserializeObject<CalculatorDto>(result!.Result.ToString());
            var statusCode = response.StatusCode;
            var costoSocioeconomico = ((3325f * 0.5f) / (0.52f * 15 * 48 * 1.1f)) * 0.1f * quintil;


            //Assert
            Assert.Equal(condicion, calculatorResponse!.Gratuidad.ToString());
            Assert.Equal(message, result.Message.FirstOrDefault());
            //Assert.Equal(15.1136f*quintil,calculatorResponse.ValorMatricula);
            //Assert.Equal((primera + segunda + tercera) * 16 * costoSocioeconomico, calculatorResponse.ValorArancel);
        }
    }
}
