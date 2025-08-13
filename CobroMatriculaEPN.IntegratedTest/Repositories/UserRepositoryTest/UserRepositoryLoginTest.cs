using Cobro_Matricula_EPN.Repository;
using Entity.DTO.User;
using Entity.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.IntegratedTest.Repositories.UserRepositoryTest
{
    public class UserRepositoryLoginTest : UserMockSetup
    {
        [Theory]
        [InlineData("jeffersoncargua@gmail.com", "UserAdmin1!", "Assistant")]
        public async Task Login_WhenSendCorrectRequest_ReturnLoginResponse(string email, string password, string role)
        {
            //Arrange
            LoginRequestDto loginRequestDto = new()
            {
                Email = email,
                Password = password
            };

            ApplicationUser user = new()
            {
                Email = email,
                Name = "example",
                LastName = "exampleLastName",
                City = "Lo que sea",
                Phone = "0987654321",
            };

            var roles = new List<string>() { role };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.IsEmailConfirmedAsync(user)).ReturnsAsync(true);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(user, password)).ReturnsAsync(true);
            _mockUserManager.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(roles);

            //Act
            var response = await repository.Login(loginRequestDto);

            //Assert
            Assert.NotNull(response.User);
            Assert.NotNull(response.Token);
            Assert.Contains("Login exitoso", response.Message);
        }

        [Theory]
        [InlineData("jeffersoncargua@gmail.com", "UserAdmin1!", "El usuario no esta registrado o el correo es incorrecto", false, false, false)]
        [InlineData("jeffersoncargua@gmail.com", "UserAdmin1!", "El usuario no ha verificado su cuenta. Revise su correo para confirmar su cuenta antes de realizar el login", true, false, false)]
        [InlineData("jeffersoncargua@gmail.com", "UserAdmin1!", "La contraseña esta incorrecta", true, true, false)]
        public async Task Login_WhenUserDoesntExistOrSendIncorrectPassword_ReturnLoginResponseDtoWithUserNull(string email, string password, string expectedMessage, bool userExist, bool isEmailConfirm, bool isCheckPasswordValid)
        {
            //Arrange
            LoginRequestDto loginRequestDto = new()
            {
                Email = email,
                Password = password
            };

            ApplicationUser user = new()
            {
                Name = "exampleName",
                LastName = "exampleLastName",
                City = "exampleCity",
                Phone = "0987654321",
                Email = email
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(userExist ? user : null);
            _mockUserManager.Setup(x => x.IsEmailConfirmedAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(isEmailConfirm);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(isCheckPasswordValid);

            //Act
            var response = await repository.Login(loginRequestDto);

            //Assert
            Assert.Null(response.User);
            Assert.Null(response.Token);
            Assert.Contains(expectedMessage, response.Message);
        }

    }
}
