using Cobro_Matricula_EPN.Controllers;
using Cobro_Matricula_EPN.Repository;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.User;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace CobroMatriculaEPN.FunctionalTest.Controller.UserControllerTest
{
    public class UserControllerWithDBTest : BaseControllerTest
    {
        private readonly HttpClient client;
        public UserControllerWithDBTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
            client = this.GetNewClient();
        }


        /// <summary>
        /// Esta prueba permite realizar el registro de un nuevo usuario, considerando que se envien todos sus parametros correctamente
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="city"></param>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <param name="confirmPass"></param>
        /// <param name="role">Este parametro si permite ser un valor null para generar un nuevo usuario</param>
        /// <returns></returns>
        [Theory]
        [InlineData("exampleFiveName", "exampleLastFiveName", "exampleFive@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assistant")]
        [InlineData("exampleTwoName", "exampleLastTwoName", "exampleTwo@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", null)]
        public async Task Register_WhenSendCorrectRequest_ReturnStatusCodeCreated(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role)
        {
            //Arrange 
            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
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

            var stringContent = new StringContent(JsonConvert.SerializeObject(registrationRequestDto),Encoding.UTF8,"application/json");


            //Act
            var response = await client.PostAsync("/api/User/Registration", stringContent);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = (int)response.StatusCode;


            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200, statusCode);
        }


        [Theory]
        [InlineData("exampleSixName", "exampleLastSixName", "exampleSix@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assistant", "Ya existe un registro con ese correo")]
        public async Task Register_WhenUserExist_ReturnStatusCodeBadRequest(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, string expectedMessage)
        {
            //Arrange
            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
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

            var stringContent = new StringContent(JsonConvert.SerializeObject(registrationRequestDto),Encoding.UTF8,"application/json");

            //Act
            //1. se crea el usuario 
            var response = await client.PostAsync("/api/User/Registration",stringContent);
            response.EnsureSuccessStatusCode();

            //2. se rea el mismo usuario para que genere el error
            var response2 = await client.PostAsync("/api/User/Registration", stringContent);
            var content = await response2.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = (int)response2.StatusCode;

            //Assert
            Assert.Equal(400, statusCode);
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Message.FirstOrDefault());

        }


        /// <summary>
        /// En esta prueba se envian los parametros invalidos para comprobar el funcionamiento de la
        /// peticion realizada al API. Se debe considerar que se estan comprobando las validaciones de 
        /// RegistrationRequestDto
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="city"></param>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <param name="confirmPass"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("exampleName1", "exampleLastName", "example@gmail.com", "exampleCity", "0987654321", "UserAdmin1", "UserAdmin1", "Assistant")]
        [InlineData("exampleName", "exampleLastName", "example@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assistant1")]
        [InlineData("exampleName", "exampleLastName", "example@gmail.com", "exampleCity", "0987654asd", "UserAdmin1!", "UserAdmin1!", null)]
        public async Task Register_WhenSendInvalidModelOrIncorrectRequest_ReturnStatusCodeBadRequest(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role)
        {
            //Arrange

            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email =  email,
                City = city,
                Phone = phone,
                Password = password,
                ConfirmPass = confirmPass,
                Role = role
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8,"application/json");


            //Act
            var response = await client.PostAsync("/api/User/Registration",stringContent);
            //var content = response.Content.ReadAsStringAsync().Result;
            //var result = JsonConvert.DeserializeObject(content);
            var statusCode = (int)response.StatusCode;

            //Assert
            Assert.Equal(400, statusCode);
        }



        /// <summary>
        /// Esta prueba permite comprobar el funcionamiento del Login de Usuario, donde se realizan 3 pasos
        /// para comprobar su registro, verificacion e inicio de sesion, para lo cual se deben enviar 
        /// 3 solicitudes HTTP correspondientes a Register, ConfirmEmail y Login respectivamente.
        /// 
        /// Otra consideracion es que se realizo la prueba de verificacion del correo por lo que no es necesario repetir 
        /// esa prueba ya que se probro su funcionamiento en esta prueba
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="city"></param>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <param name="confirmPass"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("exampleEightName", "exampleLastEightName", "exampleEight@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", null)]
        public async Task Login_WhenSendCorrectRequest_ReturnStatusCodeOk(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role)
        {
            //Arrange

            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
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

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8,"application/json");

            LoginRequestDto loginRequestDto = new()
            {
                Email = email,
                Password = password
            };

            var stringContentLogin = new StringContent(JsonConvert.SerializeObject(loginRequestDto), Encoding.UTF8, "application/json");

            //Act

            //1. Register

            var responseRegister = await client.PostAsync("/api/User/Registration",stringContentRegister);
            //EnsureSuccessStatusCode perite asegurarse de que la peticion se haya realizado correctamente
            responseRegister.EnsureSuccessStatusCode();
            //Se convierte a string el contenido de la respuesta de la peticion Post
            var contentRegister = await responseRegister.Content.ReadAsStringAsync();
            //Se convierte la informacion obtenida en el content de la respuesta del api para pasarla a un objeto APIResponse
            //para poder obtener su informacion y utilizar el token especificamente para la peticion de ConfirmEmail
            var resultRegister = JsonConvert.DeserializeObject<APIResponse>(contentRegister);
            //Se guarda la informacion del token para utilizarla en la peticion de Get ConfirmEmail
            var token = resultRegister.Result;

            //2. Confirm Email with Token

            var responseConfirmEmail = await client.GetAsync($"/api/User/ConfirmEmail?token={token}&email={email}");
            responseConfirmEmail.EnsureSuccessStatusCode();
           

            //3.Login

            var responseLogin = await client.PostAsync("/api/User/Login",stringContentLogin);
            responseLogin.EnsureSuccessStatusCode();

            var content = await responseLogin.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);
            //Nota: como el content se lee como un string el campo Result del objeto APIResponse tambien se debe deserializar
            //para pasar su informacion a un objeto LoginResponse para leer la informacion que se requiera comprobar
            var result = JsonConvert.DeserializeObject<LoginResponseDto>(apiResponse.Result.ToString());
            var statusCode = (int)responseLogin.StatusCode;


            //Assert
            Assert.Equal(200,statusCode);
            Assert.Equal("Login exitoso", apiResponse.Message.FirstOrDefault());
            Assert.NotNull(result.User);
            Assert.True(apiResponse.IsSuccess);

        }

        [Theory]
        [InlineData("exampleForteen@gmail.com","UserAdmin1!", "El usuario no esta registrado o el correo es incorrecto")]
        public async Task Login_WhenUserDoesntExist_ReturnStatusCodeBadRequest(string email, string password, string expectedMessage)
        {
            //Arrange
            //var client = this.GetNewClient();

            LoginRequestDto loginRequest = new() 
            {
                Email = email,
                Password = password
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest),Encoding.UTF8,"application/json");

            //Act
            var response = await client.PostAsync("/api/User/Login",stringContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = (int)response.StatusCode;

            //Assert
            Assert.Equal(expectedMessage, result.Message.FirstOrDefault());
            Assert.Equal(400,statusCode);

        }

        [Theory]
        [InlineData("exampleNineName", "exampleLastNineName", "exampleNine@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", null, "La contraseña esta incorrecta")]
        public async Task Login_WhenSendIncorrectPassword_ReturnStatusCodeBadRequest(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, string expectedMessage)
        {
            //Arrange

            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                Password = password,
                Role = role,
                City = city,
                Phone = phone,
                ConfirmPass = confirmPass
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto),Encoding.UTF8,"application/json");


            LoginRequestDto loginRequestDto = new()
            {
                Email = email,
                Password = "EstoNoEsElPassword1!"
            };

            var stringContentLogin = new StringContent(JsonConvert.SerializeObject(loginRequestDto), Encoding.UTF8, "application/json");

            //Act
            //1. Register

            var responseRegister = await client.PostAsync("/api/User/Registration",stringContentRegister);
            responseRegister.EnsureSuccessStatusCode();
            var contentRegister = await responseRegister.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<APIResponse>(contentRegister);
            var token = apiResponse.Result;

            //2. ConfirmEmail
            var responseConfirmEmail = await client.GetAsync($"/api/User/ConfirmEmail?token={token}&email={email}");
            responseConfirmEmail.EnsureSuccessStatusCode();

            //3. Login
            var response = await client.PostAsync("/api/User/Login",stringContentLogin);
            var contentLogin = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(contentLogin);
            var statusCode = (int)response.StatusCode;

            //Assert
            Assert.Equal(400, statusCode);
            Assert.Equal(expectedMessage, result.Message.FirstOrDefault());

        }

        [Theory]
        [InlineData("exampleElevenName", "exampleLastElevenName", "exampleEleven@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", null)]
        public async Task ResetPassword_WhenUserExistAndSendValidToken_ReturnStatusCodeOk(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role)
        {
            //Arrange

            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                Password = password,
                Role = role,
                City = city,
                Phone = phone,
                ConfirmPass = confirmPass
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8, "application/json");

            var stringContentForget = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");

            ResetPasswordRequestDto resetPasswordRequestDto = new()
            {
                Email = email,
                Password = "NewPassword1!",
                ConfirmPassword = "NewPassword1!",
            };
            
            //Act
            //1. Register
            var responseRegister = await client.PostAsync("/api/User/Registration",stringContentRegister);
            responseRegister.EnsureSuccessStatusCode();
            var content = await responseRegister.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);
            var tokenConfirmEmail = apiResponse.Result;

            //2. Confirm Email
            var responseConfirmEmail = await client.GetAsync($"/api/User/ConfirmEmail?token={tokenConfirmEmail}&email={email}");
            responseConfirmEmail.EnsureSuccessStatusCode();


            //3. ForgetPassword
            var responseForget = await client.PostAsync("/api/User/ForgetPassword",stringContentForget);
            responseForget.EnsureSuccessStatusCode();
            var contentForget = await responseForget.Content.ReadAsStringAsync();
            var resultForget = JsonConvert.DeserializeObject<APIResponse>(contentForget);
            string tokenResetPass = resultForget.Result.ToString();

            //4. ResetPassword
            resetPasswordRequestDto.Token = tokenResetPass;

            var stringContentReset = new StringContent(JsonConvert.SerializeObject(resetPasswordRequestDto),Encoding.UTF8,"application/json");

            var responseResetPass = await client.PostAsync("/api/User/ResetPassword",stringContentReset);
            responseResetPass.EnsureSuccessStatusCode();
            var contentResetPass = await responseResetPass.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(contentResetPass);
            var statusCode = (int)responseResetPass.StatusCode;


            //Assert
            Assert.Equal("Se ha actualizado su contraseña. Por favor, intente iniciar sesion", result.Message.FirstOrDefault());
            Assert.Equal(200, statusCode);
        }

        [Theory]
        [InlineData("exampleThirteenName", "exampleLastThirteenName", "exampleThirteen@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", null,true,false, "El usuario no ha confirmado su cuenta. Por favor revise su correo para verificar la cuenta")]
        [InlineData("exampleFourName", "exampleLastFourName", "exampleFour@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", null,false,false, "El usuario no se encuentra registrado")]
        public async Task ForgetPassword_WhenUserDoesntExistAndUserDoesntConfirmeEmail_ReturnStatusCodeBadRequest(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, bool userExist, bool isConfirmEmail, string expectedMessage)
        {
            //Arrange

            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                Password = password,
                Role = role,
                City = city,
                Phone = phone,
                ConfirmPass = confirmPass
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8, "application/json");

            var stringContentForget = new StringContent(JsonConvert.SerializeObject(userExist ? email : "otroexample@gmail.com"), Encoding.UTF8, "application/json");

            //ResetPasswordRequestDto resetPasswordRequestDto = new()
            //{
            //    Email = email,
            //    Password = "NewPassword1!",
            //    ConfirmPassword = "NewPassword1!",
            //};

            //Act
            //1. Register
            var responseRegister = await client.PostAsync("/api/User/Registration", stringContentRegister);
            
            var content = await responseRegister.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);
            var tokenConfirmEmail = apiResponse.Result;

            //2. Confirm Email
            if (isConfirmEmail) 
            {
                var responseConfirmEmail = await client.GetAsync($"/api/User/ConfirmEmail?token={tokenConfirmEmail}&email={email}");
            }
            
            //3. ForgetPassword
            var responseForget = await client.PostAsync("/api/User/ForgetPassword", stringContentForget);
            var contentForget = await responseForget.Content.ReadAsStringAsync();
            var resultForget = JsonConvert.DeserializeObject<APIResponse>(contentForget);
            //string tokenResetPass = resultForget.Result.ToString();
            var statusCode = (int)responseForget.StatusCode;

            //Assert
            Assert.Equal(expectedMessage, resultForget.Message.FirstOrDefault());
            Assert.Equal(400, statusCode);
        }


        [Theory]
        [InlineData("exampleName", "exampleLastName", "example@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", null, false, "El usuario no se encuentra registrado")]
        [InlineData("exampleThreeName", "exampleLastThreeName", "exampleThree@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", null, true ,"Ha ocurrido un error al cambiar su contraseña. Intentelo nuevamente")]
        public async Task ResetPassword_WhenUserDoesntExistOrInvalidToken_ReturnStatusCodeBadRequest(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, bool userExist, string expectedMessage)
        {
            //Arrange

            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                City = city,
                Phone = phone,
                Password = password,
                ConfirmPass = confirmPass,
                Role = role,
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto),Encoding.UTF8,"application/json");

            ResetPasswordRequestDto resetPasswordRequestDto = new()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPass,
                Token = "Token Invalid"
            };

            var stringContentResetPass = new StringContent(JsonConvert.SerializeObject(resetPasswordRequestDto), Encoding.UTF8, "application/json");

            //Act

            //1. Register

            if (userExist)
            {
                var responseRegister = await client.PostAsync("/api/User/Registration",stringContentRegister);
                responseRegister.EnsureSuccessStatusCode();
            }

            //2. ResetPassword
            var responseReset = await client.PostAsync("/api/User/ResetPassword",stringContentResetPass);

            var content = await responseReset.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = (int)responseReset.StatusCode;

            //Assert
            Assert.Equal(expectedMessage,result.Message.FirstOrDefault());
            Assert.Equal(400, statusCode);


        }

        [Theory]
        [InlineData("exampleName", "exampleLastName", "example@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!","Assistant")]
        public async Task UpdateUser_WhenSendCorrectRequest_ReturnStatusCodeOk(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role)
        {
            //Arrange

            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                Password = password,
                Role = role,
                City = city,
                Phone = phone,
                ConfirmPass = confirmPass
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto),Encoding.UTF8,"application/json");

            UpdateUserDto updateUserDto = new()
            {
                Name = "exampleNewName",
                LastName = "exampleNewLastName",
                City = "exampleNewCity",
                Phone = phone,
                Email = email
            };

            var stringContentUpdateUser = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");

            //Act

            //1. Register
            var responseRegister = await client.PostAsync("/api/User/Registration", stringContentRegister);
            responseRegister.EnsureSuccessStatusCode();


            //2. UpdateUser
            var responseUpdateUser = await client.PutAsync($"/api/User/UpdateUser?email={email}", stringContentUpdateUser);
            responseUpdateUser.EnsureSuccessStatusCode();
            var content = await responseUpdateUser.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<APIResponse>(content);
            var result = JsonConvert.DeserializeObject<UserDto>(apiResult.Result.ToString());
            
            var statusCode = (int)responseUpdateUser.StatusCode;

            //Assert
            Assert.Equal(200, statusCode);
            Assert.Equal("Su información ha sido actualizada",apiResult.Message.FirstOrDefault());
            Assert.IsType<UserDto>(result);


        }


        [Theory]
        [InlineData("exampleTwelveName", "exampleLastTwelveName", "exampleTwelve@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assistant",false, "El usuario no existe")]
        [InlineData("exampleTwoTwoName", "exampleLastTwoTwoName", "exampleTwoTwo@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assistant", true, "Ha ocurrido un error. No se pudo actualizar el usuario")]
        public async Task UpdateUser_WhenUserDoesntExistOrSendInvalidRequest_ReturnStatusCodeBadRequest(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role,bool userExist, string expectedMessage)
        {
            //Arrange

            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                Password = password,
                Role = role,
                City = city,
                Phone = phone,
                ConfirmPass = confirmPass
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8, "application/json");

            UpdateUserDto updateUserDto = new()
            {
                Name = "exampleNewName",
                LastName = "exampleNewName",
                City = "exampleNewCity",
                Phone = "0987654321",
                Email = !userExist ? email: "otroExample@gmail.com"
            };

            var stringContentUpdateUser = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");

            //Act

            //1. Register
            if (userExist)
            {
                var responseRegister = await client.PostAsync("/api/User/Registration", stringContentRegister);
                responseRegister.EnsureSuccessStatusCode();
            }
            

            //2. UpdateUser
            var responseUpdateUer = await client.PutAsync($"/api/User/UpdateUser?email={email}",stringContentUpdateUser);

            var content = await responseUpdateUer.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = (int)responseUpdateUer.StatusCode;


            //Assert
            Assert.Equal(400, statusCode);
            //Assert.Equal(expectedMessage, result.Message.FirstOrDefault());
            //Assert.Null(result.Result);

        }

        [Theory]
        [InlineData("exampleTenName", "exampleLastTenName", "exampleTen@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assistant")]
        public async Task DeleteUser_WhenUserExist_RemoveUserAndReturnStatusCodeOk(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role)
        {
            //Arrange

            //var client = this.GetNewClient();

            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                Password = password,
                Role = role,
                City = city,
                Phone = phone,
                ConfirmPass = confirmPass
            };

            var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8, "application/json");

            //Act

            //1. Register
            var responseRegister = await client.PostAsync("/api/User/Registration", stringContentRegister);
            responseRegister.EnsureSuccessStatusCode();

            //2.DeleteUser
            var responseDelete = await client.DeleteAsync($"/api/User/DeleteUser?email={email}");
            responseDelete.EnsureSuccessStatusCode();

            var content = await responseDelete.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = (int)responseDelete.StatusCode;

            //Assert
            Assert.Equal("El usuario ha sido eliminado de la base de datos!!!",result.Message.FirstOrDefault());
            Assert.Equal(200, statusCode);
        }


        [Fact]
        //[InlineData("exampleName", "exampleLastName", "example@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assistant")]
        public async Task DeleteUser_WhenUserDoesntExist_ReturnStatusCodeNotFound()
        {
            //Arrange

            //var client = this.GetNewClient();

            //RegistrationRequestDto registrationRequestDto = new()
            //{
            //    Name = name,
            //    LastName = lastName,
            //    Email = email,
            //    Password = password,
            //    Role = role,
            //    City = city,
            //    Phone = phone,
            //    ConfirmPass = confirmPass
            //};

            //var stringContentRegister = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8, "application/json");

            //Act

            //1. Register
            //var responseRegister = await client.PostAsync("api/User/Registration", stringContentRegister);
            //responseRegister.EnsureSuccessStatusCode();

            //2.DeleteUser
            var responseDelete = await client.DeleteAsync("/api/User/DeleteUser?email=example@gmail.com");

            var content = await responseDelete.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = (int)responseDelete.StatusCode;

            //Assert
            Assert.Equal("El usuario no existe en la base de datos!!!", result.Message.FirstOrDefault());
            Assert.Equal(404, statusCode);
        }
    }
}
