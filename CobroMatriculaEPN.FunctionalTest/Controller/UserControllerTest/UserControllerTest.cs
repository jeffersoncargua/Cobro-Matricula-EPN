using AutoMapper;
using Cobro_Matricula_EPN.Controllers;
using Cobro_Matricula_EPN.Mapping;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.User;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System.Net;
using System.Reflection;
using System.Text;
using Utility;


namespace CobroMatriculaEPN.FunctionalTest.Controller.UserControllerTest
{
    public class UserControllerTest
    {
        private readonly IConfigurationProvider _configuration;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly IMapper _mapper;
        //private readonly LoginResponseDto loginResponse;
        private readonly UserDto userDto;
        private readonly MockHttpMessageHandler _mockHttp;
        public UserControllerTest()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });

            _mapper = _configuration.CreateMapper();

            _mockUserRepository = new Mock<IUserRepository>();

            userDto = new UserDto()
            {
                Name = "exampleName",
                LastName = "exampleLastName",
                City = "exampleCity",
                Phone = "0987654321",
                Email = "example@gmail.com",
            };

            _mockHttp = new MockHttpMessageHandler();
        }

        [Theory]
        [InlineData("example@gmail.com", "UserAdmin1!")]
        public async Task Login_WhenSendCorrectCredentials_ReturnOk(string email, string password)
        {
            //Arrange
            LoginRequestDto loginRequestDto = new()
            {
                Email = email,
                Password = password
            };

            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = "aqui va el token",
                Message = "Login exitoso",
            };

            var stringContent = new StringContent((JsonConvert.SerializeObject(loginRequestDto)), Encoding.UTF8, "application/json");

            _mockHttp.When("https://localhost:7156/api/User/Login")
                .WithContent(JsonConvert.SerializeObject(loginRequestDto))
                .Respond("application/json", JsonConvert.SerializeObject(loginResponseDto));

            var client = _mockHttp.ToHttpClient();

            _mockUserRepository.Setup(x => x.Login(It.IsAny<LoginRequestDto>())).ReturnsAsync(loginResponseDto);

            var controller = new UserController(_mockUserRepository.Object);

            //Act
            var clientResponse = await client.PostAsync("https://localhost:7156/api/User/Login", stringContent);
            clientResponse.EnsureSuccessStatusCode();
            var content = await clientResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponseDto>(content);
            var statusCode = clientResponse.StatusCode;

            var apiResponse = await controller.Login(loginRequestDto);
            var response = apiResponse.Result as ObjectResult;
            var response2 = response!.Value as APIResponse;
            var result2 = response2!.Result as LoginResponseDto;
            
            var statusCode2 = (HttpStatusCode)response.StatusCode!;

            //Assert
            Assert.Equal(statusCode, statusCode2);
            Assert.NotNull(result!.User);
            Assert.NotNull(result2!.User);
            Assert.True(response2.IsSuccess);
        }

        [Theory]
        [InlineData("example@gmail.com", "UserAdmin1!", "El usuario no esta registrado o el correo es incorrecto")]
        [InlineData("example@gmail.com", "UserAdmin1!", "El usuario no ha verificado su cuenta. Revise su correo para confirmar su cuenta antes de realizar el login")]
        [InlineData("example@gmail.com", "UserAdmin1!", "La contraseña esta incorrecta")]
        public async Task Login_When_SendIncorrecCredentials_ReturnBadRequest(string email, string password, string expectedMessage)
        {
            //Arrange
            LoginRequestDto loginRequestDto = new()
            {
                Email = email,
                Password = password
            };

            LoginResponseDto loginResponseDto = new()
            {
                User = null,
                Message = expectedMessage,
                Token = null
            };

            var stringContent = new StringContent((JsonConvert.SerializeObject(loginRequestDto)), Encoding.UTF8, "application/json");

            _mockHttp.When("https://localhost:7156/api/User/Login")
                .WithContent(JsonConvert.SerializeObject(loginRequestDto))
                .Respond(HttpStatusCode.BadRequest, "application/json", JsonConvert.SerializeObject(loginResponseDto));

            var client = _mockHttp.ToHttpClient();

            _mockUserRepository.Setup(x => x.Login(It.IsAny<LoginRequestDto>())).ReturnsAsync(loginResponseDto);

            var controller = new UserController(_mockUserRepository.Object);

            //Act

            var clientResponse = await client.PostAsync("https://localhost:7156/api/User/Login", stringContent);
            var content = await clientResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponseDto>(content);
            var statusCode = clientResponse.StatusCode;

            var apiResponse = await controller.Login(loginRequestDto);
            var response = apiResponse.Result as ObjectResult;
            var result2 = response!.Value as APIResponse;
            var resultContent = result2!.Result as LoginResponseDto;
            var statusCode2 = (HttpStatusCode)response.StatusCode!;


            //Assert
            Assert.False(result2.IsSuccess);
            Assert.Equal(result!.Message!.ToString(), result2.Message.FirstOrDefault()!.ToString());
            Assert.Equal(statusCode, statusCode2);
            Assert.Null(result.User);
            Assert.Null(result.Token);

        }

        //[Theory]
        //[InlineData("example@gmail.com", "UserAdmin1!", "El usuario no existe o ha sido eliminado de la base de datos!!!")]
        //public async Task Login_WhenLoginFail_ShouldThrowException(string email, string password, string expectedMessage)
        //{
        //    //Arrange
        //    LoginRequestDto loginRequestDto = new()
        //    {
        //        Email = email,
        //        Password = password
        //    };

        //    APIResponse aPIResponse = new()
        //    {
        //        IsSuccess = false,
        //        Message = new List<string>() { "El usuario no existe o ha sido eliminado de la base de datos!!!" },
        //        Result = null,
        //        StatusCode = HttpStatusCode.NotFound
        //    };

        //    //Cuando se lanza una excepcion se debe verificar lo que se retorna dentro del catch, ya que de eso dependerá
        //    //lo que se desea comprobar con assert
        //    _mockUserRepository.Setup(x => x.Login(It.IsAny<LoginRequestDto>())).Throws(new Exception());

        //    var controller = new UserController(_mockUserRepository.Object);

        //    //Act

        //    //var action = async () => await controller.Login(loginRequestDto);
        //    var response = await controller.Login(loginRequestDto);
        //    var result = response.Result as ObjectResult;
        //    var resultContent = result.Value as APIResponse;



        //    //Assert
        //    Assert.Equal(resultContent.StatusCode, aPIResponse.StatusCode);
        //    Assert.Equal(resultContent.IsSuccess, aPIResponse.IsSuccess);
        //    Assert.Equal(resultContent.Message, aPIResponse.Message);
        //    Assert.Null(resultContent.Result);

        //}

        [Theory]
        [InlineData("exampleName", "exampleLastName", "example@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assitant")]
        public async Task Register_WhenSendCorrectRequest_ReturnStatusCodeCreated(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role)
        {
            //Arrange
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

            APIResponse aPIResponse = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created,
                Result = null,
                Message = new List<string>()
                {
                    "Registro Exitoso"
                }
            };

            RegisterResponseDto registerResponseDto = new()
            {
                Success = true,
                MessageResponse = new List<string>() { "Registro Exitoso" }
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8, "application/json");

            _mockHttp.When("https://localhost:7156/api/User/Registration")
                .WithContent(JsonConvert.SerializeObject(registrationRequestDto))
                .Respond("application/json", JsonConvert.SerializeObject(aPIResponse));

            _mockUserRepository.Setup(x => x.Register(It.IsAny<RegistrationRequestDto>())).ReturnsAsync(registerResponseDto);

            var client = _mockHttp.ToHttpClient();

            var controller = new UserController(_mockUserRepository.Object);

            //Act
            var clientResponse = await client.PostAsync("https://localhost:7156/api/User/Registration", stringContent);
            clientResponse.EnsureSuccessStatusCode();
            var content = await clientResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = clientResponse.StatusCode;

            var response = await controller.Registration(registrationRequestDto);
            var result2 = response.Result as ObjectResult;
            var resultContent = result2!.Value as APIResponse;
            var statusCode2 = (HttpStatusCode)result2.StatusCode!;

            //Assert
            Assert.Equal(result!.IsSuccess, resultContent!.IsSuccess);
            Assert.Equal(result.Message, resultContent.Message);
            Assert.Equal(result.StatusCode, resultContent.StatusCode);
            Assert.Equal(statusCode, statusCode2);

        }


        [Theory]
        [InlineData("exampleName", "exampleLastName", "example@gmail.com", "exampleCity", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assitant", "Ya existe un registro con ese correo")]
        [InlineData("e", "example2222", "example", "exampleCity", "09876uno", "UserAdmin1", "UserAdmin1!!", null, "Ya existe un registro con ese correo")]
        public async Task Register_WhenSendIncorrectRequest_ReturnBadRequest(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, string expectedMessage)
        {
            //Arrange
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

            RegisterResponseDto registerResponseDto = new()
            {
                Success = false,
                MessageResponse = new List<string>() { expectedMessage }
            };

            APIResponse aPIResponse = new()
            {
                IsSuccess = false,
                Message = new List<string>() { expectedMessage },
                StatusCode = HttpStatusCode.BadRequest,
                Result = null
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(registrationRequestDto), Encoding.UTF8, "application/json");

            _mockHttp.When("https://localhost:7156/api/User/Registration")
                .WithContent(JsonConvert.SerializeObject(registrationRequestDto))
                .Respond(HttpStatusCode.BadRequest, "aplication/json", JsonConvert.SerializeObject(aPIResponse));

            var client = _mockHttp.ToHttpClient();
            _mockUserRepository.Setup(x => x.Register(It.IsAny<RegistrationRequestDto>())).ReturnsAsync(registerResponseDto);

            var controller = new UserController(_mockUserRepository.Object);

            //Act
            var clientResponse = await client.PostAsync("https://localhost:7156/api/User/Registration", stringContent);

            var content = await clientResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = clientResponse.StatusCode;

            var response = await controller.Registration(registrationRequestDto);
            var result2 = response.Result as ObjectResult;
            var resultContent = result2!.Value as APIResponse;
            var statusCode2 = (HttpStatusCode)result2.StatusCode!;


            //Assert

            Assert.Equal(resultContent!.StatusCode, result!.StatusCode);

        }


        [Theory]
        [InlineData("aqui va el token", "exmaple@gmail.com")]
        public async Task ConfirmEmail_WhenSendTokenAndEmailValid_ReturnStatusCodeOk(string token, string email)
        {
            //Arrange
            APIResponse aPIResponse = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = null,
                Message = ["La cuenta ha sido verificada!!"]
            };

            _mockHttp.When($"https://localhost:7156/api/User/ConfirmEmail?token={token}&email={email}")
                .Respond("application/json", JsonConvert.SerializeObject(aPIResponse));

            var client = _mockHttp.ToHttpClient();

            _mockUserRepository.Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            var controller = new UserController(_mockUserRepository.Object);

            //Act
            var clientResponse = await client.GetAsync($"https://localhost:7156/api/User/ConfirmEmail?token={token}&email={email}");
            clientResponse.EnsureSuccessStatusCode();

            var content = await clientResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = clientResponse.StatusCode;

            var response = await controller.ConfirmEmail(token, email);
            var result2 = response.Result as ObjectResult;
            var resultContent = result2!.Value as APIResponse;
            var statusCode2 = (HttpStatusCode)result2.StatusCode!;

            //Assert
            Assert.Equal(statusCode, statusCode2);
            Assert.Equal(result!.Message, resultContent!.Message);
            Assert.Equal(result.IsSuccess, resultContent.IsSuccess);

        }

        [Fact]
        public async Task ConfirmEmail_WhenSendTokenAndEmailInvalid_ReturnStatusCodeBadRequest()
        {
            //Arrange
            APIResponse aPIResponse = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Result = null,
                Message = ["No existe una cuenta registrada!!"]
            };

            _mockHttp.When($"https://localhost:7156/api/User/ConfirmEmail")
                .Respond(HttpStatusCode.BadRequest, "application/json", JsonConvert.SerializeObject(aPIResponse));
            var client = _mockHttp.ToHttpClient();

            _mockUserRepository.Setup(x => x.ConfirmEmailAsync(null, null)).ReturnsAsync(false);
            var controller = new UserController(_mockUserRepository.Object);


            //Act
            var clientResponse = await client.GetAsync("https://localhost:7156/api/User/ConfirmEmail");
            var content = await clientResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = clientResponse.StatusCode;

            var response = await controller.ConfirmEmail(null, null);
            var result2 = response.Result as ObjectResult;
            var resultContent = result2!.Value as APIResponse;
            var statusCode2 = (HttpStatusCode)result2.StatusCode!;


            //Assert
            Assert.Equal(statusCode2, statusCode);
            Assert.Equal(result!.Message, resultContent!.Message);
            Assert.Equal(result.IsSuccess, resultContent.IsSuccess);
        }

        [Theory]
        [InlineData("examppple@gmail.com")]
        public async Task ForgetPassword_WhenSendEmailValid_ReturnStatusCodeOK(string email)
        {
            //Arrange
            APIResponse aPIResponse = new()
            {
                IsSuccess = true,
                Message = ["Solicitud aceptada. Por favor revice su correo para continuar con el proceso de cambio de contraseña"],
                StatusCode = HttpStatusCode.OK,
                Result = null
            };

            ForgetResponseDto forgetResponseDto = new()
            {
                Success = true,
                Message = "Solicitud aceptada. Por favor revice su correo para continuar con el proceso de cambio de contraseña"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");

            _mockHttp.When("https://localhost:7156/api/User/ForgetPassword")
                .WithContent(JsonConvert.SerializeObject(email))
                .Respond("application/json", JsonConvert.SerializeObject(aPIResponse));

            var client = _mockHttp.ToHttpClient();

            _mockUserRepository.Setup(x => x.ForgetPasswordAsyn(It.IsAny<string>())).ReturnsAsync(forgetResponseDto);
            var controller = new UserController(_mockUserRepository.Object);

            //Act
            var clientResponse = await client.PostAsync("https://localhost:7156/api/User/ForgetPassword", stringContent);
            clientResponse.EnsureSuccessStatusCode();
            var content = await clientResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = clientResponse.StatusCode;


            var response = await controller.ForgetPassword(email);
            var result2 = response.Result as ObjectResult;
            var resultContent = result2!.Value as APIResponse;
            var statusCode2 = (HttpStatusCode)result2.StatusCode!;

            //Assert
            Assert.Equal(statusCode, statusCode2);
            Assert.Equal(result!.Message, resultContent!.Message);
            Assert.Equal(result.IsSuccess, resultContent.IsSuccess);

        }


        [Theory]
        [InlineData("exampleError@gmail.com", "El usuario no ha confirmado su cuenta. Por favor revise su correo para verificar la cuenta")]
        [InlineData("exampleError@gmail.com", "El usuario no se encuentra registrado")]
        public async Task ForgetPassword_WhenUserDoesntExist_ReturnStatusCodeBadRequest(string email, string expecteMessage)
        {
            //Arrange
            APIResponse aPIResponse = new()
            {
                IsSuccess = false,
                Message = [expecteMessage],
                StatusCode = HttpStatusCode.NotFound,
                Result = null
            };

            ForgetResponseDto forgetResponseDto = new ()
            {
                Success = false,
                Message = expecteMessage
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");

            _mockHttp.When("https://localhost:7156/api/User/ForgetPassword")
                .WithContent(JsonConvert.SerializeObject(email))
                .Respond(HttpStatusCode.BadRequest, "application/json", JsonConvert.SerializeObject(aPIResponse));

            var client = _mockHttp.ToHttpClient();

            _mockUserRepository.Setup(x => x.ForgetPasswordAsyn(It.IsAny<string>())).ReturnsAsync(forgetResponseDto);
            var controller = new UserController(_mockUserRepository.Object);

            //Act
            var clientResponse = await client.PostAsync("https://localhost:7156/api/User/ForgetPassword", stringContent);
            var content = await clientResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = clientResponse.StatusCode;


            var response = await controller.ForgetPassword(email);
            var result2 = response.Result as ObjectResult;
            var resultContent = result2!.Value as APIResponse;
            var statusCode2 = (HttpStatusCode)result2.StatusCode!;

            //Assert
            Assert.Equal(result!.Message, resultContent!.Message);
            Assert.Equal(statusCode, statusCode2);
            Assert.Equal(result.IsSuccess, resultContent.IsSuccess);
        }


        [Theory]
        [InlineData("expample@gmail.com", "userAdmin1!", "userAdmin1!", "Aqui va el token")]
        public async Task ResetPasword_WhenSendCorrectRequest_RetunrStatusCodeOK(string email, string password, string confirmPassword, string token)
        {
            //Arrange
            APIResponse aPIResponse = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = ["Se ha actualizado su contraseña. Por favor, intente iniciar sesion"],
                Result = null
            };

            ResetPasswordRequestDto resetPasswordRequest = new()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword,
                Token = token
            };

            ResetPasswordResponseDto resetPasswordResponseDto = new()
            {
                Success = true,
                Message = "Se ha actualizado su contraseña. Por favor, intente iniciar sesion"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(resetPasswordRequest), Encoding.UTF8, "application/json");

            _mockHttp.When("https://localhost:7156/api/User/ResetPassword")
                .WithContent(JsonConvert.SerializeObject(resetPasswordRequest))
                .Respond("application/json", JsonConvert.SerializeObject(aPIResponse));

            var client = _mockHttp.ToHttpClient();

            _mockUserRepository.Setup(x => x.ResetPasswordAsync(It.IsAny<ResetPasswordRequestDto>())).ReturnsAsync(resetPasswordResponseDto);

            var controller = new UserController(_mockUserRepository.Object);

            //Act
            var clientResponse = await client.PostAsync("https://localhost:7156/api/User/ResetPassword", stringContent);
            clientResponse.EnsureSuccessStatusCode();
            var content = await clientResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = clientResponse.StatusCode;

            var response = await controller.ResetPassword(resetPasswordRequest);
            var result2 = response.Result as ObjectResult;
            var resultContent = result2!.Value as APIResponse;
            var statusCode2 = (HttpStatusCode)result2.StatusCode!;


            //Assert
            Assert.Equal(statusCode, statusCode2);
            Assert.Equal(result!.Message, resultContent!.Message);
            Assert.Equal(result.IsSuccess, resultContent.IsSuccess);

        }

        [Theory]
        [InlineData("expample@gmail.com", "userAdmin1!", "userAdmin1!", "Aqui va el token", "El usuario no se encuentra registrado")]
        [InlineData("expample@gmail.com", "userAdmin1!", "userAdmin1!", "Aqui va el token", "Ha ocurrido un error al cambiar su contraseña. Intentelo nuevamente")]
        public async Task ResetPassword_WhenSendIncorrectRequest_ReturnStatusCodeBadRequest(string email, string password, string confirmPassword, string token, string expectedMessage)
        {
            //Arrane
            APIResponse aPIResponse = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = [expectedMessage],
                Result = null
            };

            ResetPasswordRequestDto resetPasswordRequest = new()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword,
                Token = token
            };

            ResetPasswordResponseDto resetPasswordResponseDto = new()
            {
                Success = false,
                Message = expectedMessage
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(resetPasswordRequest), Encoding.UTF8, "application/json");

            _mockHttp.When("https://localhost:7156/api/User/ResetPassword")
                .WithContent(JsonConvert.SerializeObject(resetPasswordRequest))
                .Respond(HttpStatusCode.BadRequest, "application/json", JsonConvert.SerializeObject(aPIResponse));

            var client = _mockHttp.ToHttpClient();

            _mockUserRepository.Setup(x => x.ResetPasswordAsync(It.IsAny<ResetPasswordRequestDto>())).ReturnsAsync(resetPasswordResponseDto);

            var controller = new UserController(_mockUserRepository.Object);

            //Act
            var clientResponse = await client.PostAsync("https://localhost:7156/api/User/ResetPassword", stringContent);
            var content = await clientResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse>(content);
            var statusCode = clientResponse.StatusCode;

            var response = await controller.ResetPassword(resetPasswordRequest);
            var result2 = response.Result as ObjectResult;
            var resultContent = result2!.Value as APIResponse;
            var statusCode2 = (HttpStatusCode)result2.StatusCode!;


            //Assert
            Assert.Equal(statusCode, statusCode2);
            Assert.Equal(result!.Message, resultContent!.Message);
            Assert.Equal(result.IsSuccess, resultContent.IsSuccess);
        }

    }
}
