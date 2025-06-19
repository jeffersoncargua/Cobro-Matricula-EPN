using Entity.DTO.User;
using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.IntegratedTest.Repositories.UserRepositoryTest
{
    public class UserRepositoryResetPasswordTest : UserMockSetup
    {
        [Theory]
        [InlineData("example@gmail.com","UserAdmin1!","UserAdmin1!","Aqui va el token")]
        public async Task ResetPassword_WhenSendCorrectRequest_ReturnCorrectResetResponse(string email,string password, string confirmPass, string token)
        {
            //Arrange
            ResetPasswordRequestDto resetPasswordRequestDto = new()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPass,
                Token = token
            };

            ResetPasswordResponseDto responseDto = new()
            {
                Success = true,
                Message = "Se ha actualizado su contraseña. Por favor, intente iniciar sesion"
            };

            ApplicationUser user = new()
            {
                Email = email,
                Name = "exampleName",
                LastName = "exampleLastName",
                Phone = "0987654321",
                City = "exampleCity",
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var response = await repository.ResetPasswordAsync(resetPasswordRequestDto);

            //Assert
            Assert.True(response.Success);
            Assert.Contains(responseDto.Message, response.Message);
        }

        [Theory]
        [InlineData(false , "El usuario no se encuentra registrado")]
        [InlineData(true, "Ha ocurrido un error al cambiar su contraseña. Intentelo nuevamente")]
        public async Task ResetPassword_WhenUserDoesntExistOrResetFailed_ReturnSuccessEqualFalse(bool userExist, string expectedMessage)
        {
            //Arrange
            ApplicationUser user = new()
            {
                Name = "exampleName",
                LastName = "exampleLastName",
                City = "exampleCity",
                Phone = "0987654321",
                Email = "example@gmail.com",
            };

            ResetPasswordRequestDto resetPasswordRequestDto = new()
            {
                Email = "example@gmail.com",
                Password = "PasswordExample1!",
                ConfirmPassword = "PasswordExample1!",
                Token = "Aqui va un token"
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(userExist ? user : null);
            _mockUserManager.Setup(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed()));

            //Act
            var response = await repository.ResetPasswordAsync(resetPasswordRequestDto);


            //Assert
            Assert.False(response.Success);
            Assert.Contains(expectedMessage, response.Message);
        }

    }
}
