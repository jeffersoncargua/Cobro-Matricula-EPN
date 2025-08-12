using Entity.DTO.BaseParameter;
using Entity.DTO.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace CobroMatriculaEPN.FunctionalTest.Controller.BaseParameterControllerTest
{
    
    public class BaseParameterControllerTest : BaseControllerTest
    {
        public BaseParameterControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("exampleEighteenName", "exampleLastEighteenName", "exampleEighteen@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", null,1)]
        [InlineData("exampleSevenTeenName", "exampleLastSevenTeenName", "exampleSevenTeen@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Admin",2)]
        public async Task GetParameters_WhenParameterIdExist_ReturnStatusCodeOk(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, int parameterId)
        {
            //Arrange
            var client = this.GetNewClient();

            RegistrationRequestDto registrationRequest = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                City = city,
                Phone = phone,
                Password = password,
                ConfirmPass = confirmPass,
                Role = role
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequest), Encoding.UTF8,"application/json");

            LoginRequestDto loginRequest = new()
            {
                Email = email,
                Password = password
            };

            var stringContentLogin = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");


            //Act

            //1. Register User
            var apiRegisterResponse = await client.PostAsync("/api/User/Registration", stringContentRegister);
            apiRegisterResponse.EnsureSuccessStatusCode();
            var content3 = await apiRegisterResponse.Content.ReadAsStringAsync();
            var resultApiRegister = JsonConvert.DeserializeObject<APIResponse>(content3);
            var tokenConfirm = resultApiRegister!.Result;

            //2. Confirm Email 
            var apiConfirmResponse = await client.GetAsync($"/api/User/ConfirmEmail?token={tokenConfirm}&email={email}");
            apiConfirmResponse.EnsureSuccessStatusCode();


            //3. Login for getting token

            var apiLoginResponse = await client.PostAsync("/api/User/Login", stringContentLogin);
            apiLoginResponse.EnsureSuccessStatusCode();

            var content1 = await apiLoginResponse.Content.ReadAsStringAsync();
            var resultApiLogin = JsonConvert.DeserializeObject<APIResponse>(content1);
            var resultLogin = JsonConvert.DeserializeObject<LoginResponseDto>(resultApiLogin!.Result.ToString());
            var token = resultLogin!.Token;


            //4. GetParameters with Authorization 
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"/api/BaseParameter/GetParameters/{parameterId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var statusCode = response.StatusCode;
            var resultGetParameters = JsonConvert.DeserializeObject<APIResponse>(content);
            var result = JsonConvert.DeserializeObject<BaseParameterDto>(resultGetParameters!.Result.ToString());



            //Assert
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.IsType<BaseParameterDto>(result);
        }


        [Theory]
        [InlineData("exampleSixteenName", "exampleLastSixteenName", "exampleSixteen@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", null, 4)]
        [InlineData("exampleFifteenName", "exampleLastFifteenName", "exampleFifteen@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Admin", 27)]
        public async Task GetParameters_WhenParameterIdDoesntExist_ReturnStatusCodeNotFound(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, int parameterId)
        {
            //Arrange
            var client = this.GetNewClient();

            RegistrationRequestDto registrationRequest = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                City = city,
                Phone = phone,
                Password = password,
                ConfirmPass = confirmPass,
                Role = role
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequest), Encoding.UTF8, "application/json");

            LoginRequestDto loginRequest = new()
            {
                Email = email,
                Password = password
            };

            var stringContentLogin = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");


            //Act

            //1. Register User
            var apiRegisterResponse = await client.PostAsync("/api/User/Registration", stringContentRegister);
            apiRegisterResponse.EnsureSuccessStatusCode();
            var content3 = await apiRegisterResponse.Content.ReadAsStringAsync();
            var resultApiRegister = JsonConvert.DeserializeObject<APIResponse>(content3);
            var tokenConfirm = resultApiRegister!.Result;

            //2. Confirm Email 
            var apiConfirmResponse = await client.GetAsync($"/api/User/ConfirmEmail?token={tokenConfirm}&email={email}");
            apiConfirmResponse.EnsureSuccessStatusCode();


            //3. Login for getting token

            var apiLoginResponse = await client.PostAsync("/api/User/Login", stringContentLogin);
            apiLoginResponse.EnsureSuccessStatusCode();

            var content1 = await apiLoginResponse.Content.ReadAsStringAsync();
            var resultApiLogin = JsonConvert.DeserializeObject<APIResponse>(content1);
            var resultLogin = JsonConvert.DeserializeObject<LoginResponseDto>(resultApiLogin!.Result.ToString());
            var token = resultLogin!.Token;


            //4. GetParameters with Authorization 
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"/api/BaseParameter/GetParameters/{parameterId}");
            var content = await response.Content.ReadAsStringAsync();
            var statusCode = response.StatusCode;
            var resultGetParameters = JsonConvert.DeserializeObject<APIResponse>(content);



            //Assert
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
            Assert.Equal("No se ha encontrado la información de los parametros.", resultGetParameters!.Message[0]);
        }


        #region Pruebas para actualizar los parametros base 

        /// <summary>
        /// Esta prueba permite comprobar la actualizacion de la informacion de los parametros base, para lo cual debe ser un usuario Administrador
        /// y este debe ingresar correctamente la informacion de los parametros de UpdatedBaseParametersRequestDto para realizar esta accion, 
        /// caso contrario dara un error
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formacionAcademica"></param>
        /// <param name="costoOptimo"></param>
        /// <param name="horaPeriodoAcademico"></param>
        /// <param name="creditoPeriodoAcademico"></param>
        /// <param name="porcentajeCostoOptimoAnual"></param>
        /// <param name="porcentajeValorMin"></param>
        /// <param name="porcentajeValorMax"></param>
        /// <param name="porcentajeValorArancel"></param>
        /// <param name="porcentajePromedioAcademico"></param>
        /// <param name="porcentajePerdidaTemporal"></param>
        /// <param name="porcentajeMatriculaExtraordinario"></param>
        /// <param name="porcentajeMatriculaEspecial"></param>
        /// <param name="porcentajeRecargoSegunda"></param>
        /// <param name="porcentajeRecargoTercera"></param>
        /// <param name="role"></param>
        /// <param name="numberOfAdmin"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1, "Otra Formacion Academica",3350.54f,725,25,0.56f,0.11f,0.51f,0.11f,0.53f,0.30f,0.26f,0.26f,0.16f,0.22f, "Admin",1)]
        [InlineData(1, "Otra Formacion Academica 2", 3360f, 725, 25, 0.56f, 0.12f, 0.53f, 0.11f, 0.56f, 0.30f, 0.26f, 0.26f, 0.16f, 0.22f, "Admin",2)]
        public async Task UpdateParameters_WhenSendCorrectRequest_ReturnStatusCodeOk(int id, string formacionAcademica, float costoOptimo, int horaPeriodoAcademico, int creditoPeriodoAcademico, float porcentajeCostoOptimoAnual, float porcentajeValorMin, float porcentajeValorMax, float porcentajeValorArancel, float porcentajePromedioAcademico, float porcentajePerdidaTemporal, float porcentajeMatriculaExtraordinario, float porcentajeMatriculaEspecial, float porcentajeRecargoSegunda, float porcentajeRecargoTercera, string role, int numberOfAdmin)
        {
            //Arrange
            var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = "UserName",
                LastName = "UserLastName",
                City = "UserCity",
                Phone = "0987654321",
                Password = "UserAdmin1!",
                ConfirmPass = "UserAdmin1!",
                Email = $"userExample{numberOfAdmin}@gmail.com",
                Role = role
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8, "application/json");

            LoginRequestDto loginRequest = new()
            {
                Email = $"userExample{numberOfAdmin}@gmail.com",
                Password = "UserAdmin1!"
            };

            var stringContentLogin = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            UpdatedBaseParameterRequestDto requestDto = new()
            {
                Id = id,
                FormacionAcademica = formacionAcademica,
                CostoOptimo = costoOptimo,
                HoraPeriodoAcademico = horaPeriodoAcademico,
                CreditoPeriodoAcademico = creditoPeriodoAcademico,
                PorcentajeCostoOptimoAnual = porcentajeCostoOptimoAnual,
                PorcentajeValorMin = porcentajeValorMin,
                PorcentajeValorMax = porcentajeValorMax,
                PorcentajeValorArancel = porcentajeValorArancel,
                PorcentajePromedioAcademico = porcentajePromedioAcademico,
                PorcentajePerdidaTemporal = porcentajePerdidaTemporal,
                PorcentajeMatriculaEspecial = porcentajeMatriculaEspecial,
                PorcentajeMatriculaExtraordinario = porcentajeMatriculaExtraordinario,
                PorcentajeRecargoSegunda = porcentajeRecargoSegunda,
                PorcentajeRecargoTercera= porcentajeRecargoTercera
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(requestDto), Encoding.UTF8, "application/json");


            //Act

            //1. Register User

            var apiResponseRegister = await client.PostAsync("/api/User/Registration",stringContentRegister);
            apiResponseRegister.EnsureSuccessStatusCode();
            var contentRegister = await apiResponseRegister.Content.ReadAsStringAsync();
            var resultRegister = JsonConvert.DeserializeObject<APIResponse>(contentRegister);
            var tokenEmail = resultRegister!.Result;


            //2.Confirm Email

            var apiConfirmResponse = await client.GetAsync($"/api/User/ConfirmEmail?token={tokenEmail}&email={registrationRequestDto.Email}");
            apiConfirmResponse.EnsureSuccessStatusCode();

            //3.Login 
            var apiLoginResponse = await client.PostAsync("/api/User/Login", stringContentLogin);
            apiLoginResponse.EnsureSuccessStatusCode();
            var contentLogin = await apiLoginResponse.Content.ReadAsStringAsync();
            var resultLogin = JsonConvert.DeserializeObject<APIResponse>(contentLogin);
            var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(resultLogin!.Result.ToString());
            var token = loginResponse!.Token;


            //4. Update Base Parameters
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token);
            var apiUpdateResponse = await client.PutAsync($"/api/BaseParameter/UpdateParameters/{id}",stringContent);
            apiUpdateResponse.EnsureSuccessStatusCode();
            var contentUpdateParameters = await apiUpdateResponse.Content.ReadAsStringAsync();
            var resultUpdateParameters = JsonConvert.DeserializeObject<APIResponse>(contentUpdateParameters);
            var resultParameters = JsonConvert.DeserializeObject<BaseParameterDto>(resultUpdateParameters!.Result.ToString());
            var statusCode = resultUpdateParameters.StatusCode;


            //Assert
            Assert.Equal("Los Parametros han sido actualizados correctamente.", resultUpdateParameters.Message[0]);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(costoOptimo, resultParameters!.CostoOptimo);
        }


        [Theory]
        [InlineData(3, "Otra Formacion Academica", 3350.54f, 725, 25, 0.56f, 0.11f, 0.51f, 0.11f, 0.53f, 0.30f, 0.26f, 0.26f, 0.16f, 0.22f, "Admin", "Two")]
        [InlineData(4, "Otra Formacion Academica 2", 3360f, 725, 25, 0.56f, 0.12f, 0.53f, 0.11f, 0.56f, 0.30f, 0.26f, 0.26f, 0.16f, 0.22f, "Admin", "Three")]
        [InlineData(1, "Otra Formacion Academica 3", 3360f, 725, 25, 0.56f, 0.12f, 0.53f, 0.11f, 0.56f, 0.30f, 0.26f, 0.26f, 0.16f, 0.22f, "Admin", "Four")]
        public async Task UpdateParameters_WhenParameterIdDoesntExistOrParameterIdIsDifferentInRequest_ReturnStatusBadRquest(int id, string formacionAcademica, float costoOptimo, int horaPeriodoAcademico, int creditoPeriodoAcademico, float porcentajeCostoOptimoAnual, float porcentajeValorMin, float porcentajeValorMax, float porcentajeValorArancel, float porcentajePromedioAcademico, float porcentajePerdidaTemporal, float porcentajeMatriculaExtraordinario, float porcentajeMatriculaEspecial, float porcentajeRecargoSegunda, float porcentajeRecargoTercera, string role, string numberOfUser)
        {
            //Arrange
            var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = $"User{numberOfUser}Name",
                LastName = $"UserLast{numberOfUser}Name",
                City = "UserCity",
                Phone = "0987654321",
                Password = "UserAdmin1!",
                ConfirmPass = "UserAdmin1!",
                Email = $"userExample{numberOfUser}@gmail.com",
                Role = role
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8, "application/json");

            LoginRequestDto loginRequest = new()
            {
                Email = $"userExample{numberOfUser}@gmail.com",
                Password = "UserAdmin1!"
            };

            var stringContentLogin = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            UpdatedBaseParameterRequestDto requestDto = new()
            {
                Id = id,
                FormacionAcademica = formacionAcademica,
                CostoOptimo = costoOptimo,
                HoraPeriodoAcademico = horaPeriodoAcademico,
                CreditoPeriodoAcademico = creditoPeriodoAcademico,
                PorcentajeCostoOptimoAnual = porcentajeCostoOptimoAnual,
                PorcentajeValorMin = porcentajeValorMin,
                PorcentajeValorMax = porcentajeValorMax,
                PorcentajeValorArancel = porcentajeValorArancel,
                PorcentajePromedioAcademico = porcentajePromedioAcademico,
                PorcentajePerdidaTemporal = porcentajePerdidaTemporal,
                PorcentajeMatriculaEspecial = porcentajeMatriculaEspecial,
                PorcentajeMatriculaExtraordinario = porcentajeMatriculaExtraordinario,
                PorcentajeRecargoSegunda = porcentajeRecargoSegunda,
                PorcentajeRecargoTercera = porcentajeRecargoTercera
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(requestDto), Encoding.UTF8, "application/json");

            //Act
            //1. Register User

            var apiResponseRegister = await client.PostAsync("/api/User/Registration", stringContentRegister);
            apiResponseRegister.EnsureSuccessStatusCode();
            var contentRegister = await apiResponseRegister.Content.ReadAsStringAsync();
            var resultRegister = JsonConvert.DeserializeObject<APIResponse>(contentRegister);
            var tokenEmail = resultRegister!.Result;


            //2.Confirm Email

            var apiConfirmResponse = await client.GetAsync($"/api/User/ConfirmEmail?token={tokenEmail}&email={registrationRequestDto.Email}");
            apiConfirmResponse.EnsureSuccessStatusCode();

            //3.Login 
            var apiLoginResponse = await client.PostAsync("/api/User/Login", stringContentLogin);
            apiLoginResponse.EnsureSuccessStatusCode();
            var contentLogin = await apiLoginResponse.Content.ReadAsStringAsync();
            var resultLogin = JsonConvert.DeserializeObject<APIResponse>(contentLogin);
            var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(resultLogin!.Result.ToString());
            var token = loginResponse!.Token;


            //4. Update Base Parameters
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var apiUpdateResponse = await client.PutAsync($"/api/BaseParameter/UpdateParameters/{(id == 1 ? 2 : id)}", stringContent);
            var contentUpdateParameters = await apiUpdateResponse.Content.ReadAsStringAsync();
            var resultUpdateParameters = JsonConvert.DeserializeObject<APIResponse>(contentUpdateParameters);
            var statusCode = resultUpdateParameters!.StatusCode;


            //Assert
            Assert.Equal("No se encontró la información de los parametros a actualizar.", resultUpdateParameters.Message[0]);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            Assert.Null(resultUpdateParameters.Result);

        }

        #endregion
    }

}
