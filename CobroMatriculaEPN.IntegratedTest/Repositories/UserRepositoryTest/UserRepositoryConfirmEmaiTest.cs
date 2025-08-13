using Cobro_Matricula_EPN.Repository;
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
    public class UserRepositoryConfirmEmaiTest : UserMockSetup
    {
        [Theory]
        [InlineData("aqui va el token", "example@gmail.com", true)]
        public async Task ConfirmEmail_WhenUserExistAndSendCorrectToken_ReturnTrue(string token, string email, bool userExist)
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

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var response = await repository.ConfirmEmailAsync(token, email);

            //Assert
            Assert.True(response);
        }

        [Theory]
        [InlineData("Aqui va un Token", "example@gmail.com", true)]
        [InlineData("aqui va el token", "example@gmail.com", false)]
        [InlineData(null, "example@gmail.com", true)]
        [InlineData("", "example@gmail.com", true)]
        public async Task ConfirmEmail_WhenUserDoestExistOrSendIncorrectorNullToken_ReturnFalse(string token, string email, bool userExist)
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

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(userExist ? user : null);
            _mockUserManager.Setup(x => x.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed()));

            //Act
            var response = await repository.ConfirmEmailAsync(token, email);

            //Assert
            Assert.False(response);
        }
    }
}
