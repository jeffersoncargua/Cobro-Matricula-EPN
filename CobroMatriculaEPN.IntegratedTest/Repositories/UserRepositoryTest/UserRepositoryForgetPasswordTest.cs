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
    public class UserRepositoryForgetPasswordTest : UserMockSetup
    {
        [Theory]
        [InlineData("example@gmail.com")]
        public async Task ForgetPassword_WhenUserExist_ReturnSuccessEqualTrue(string email)
        {
            //Arrange
            ApplicationUser user = new()
            {
                Name = "exampleName",
                LastName = "exampleLastName",
                City = "exampleCity",
                Phone = "0987654321",
                Email = email
            };

            ForgetResponseDto forgetResponseDto = new()
            {
                Success = true,
                Message = "Solicitud aceptada. Por favor revice su correo para continuar con el proceso de cambio de contraseña"
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.IsEmailConfirmedAsync(user)).ReturnsAsync(true);


            //Act
            var response = await repository.ForgetPasswordAsyn(email);

            //Assert
            Assert.True(response.Success);
            Assert.Contains(forgetResponseDto.Message, response.Message);

        }

        [Theory]
        [InlineData("example@gmail.com", false, "El usuario no se encuentra registrado")]
        [InlineData("example@gmail.com", true, "El usuario no ha confirmado su cuenta. Por favor revise su correo para verificar la cuenta")]
        public async Task ForgetPassword_WhenUserDoesntExistOrEmailDoesntConfirm_ReturnSuccessEqualFalse(string email, bool userExist, string expectedMessage)
        {
            //Arrange
            ApplicationUser user = new()
            {
                Name = "exampleName",
                LastName = "exampleLastName",
                City = "exampleCity",
                Phone = "0987654321",
                Email = email
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(userExist ? user : null);
            _mockUserManager.Setup(x => x.IsEmailConfirmedAsync(user)).ReturnsAsync(false);

            //Act
            var response = await repository.ForgetPasswordAsyn(email);

            //Assert
            Assert.False(response.Success);
            Assert.Contains(expectedMessage, response.Message);
        }
    }
}
