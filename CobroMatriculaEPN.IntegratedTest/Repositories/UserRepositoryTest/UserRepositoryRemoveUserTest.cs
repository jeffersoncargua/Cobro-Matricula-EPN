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
    public class UserRepositoryRemoveUserTest : UserMockSetup
    {
        private readonly ApplicationUser user;

        public UserRepositoryRemoveUserTest()
        {
            user = new ApplicationUser()
            {
                Name = "exampleName",
                LastName = "exampleLastName",
                Email = "example@gmial.com",
                City = "exampleCity",
                Phone = "0987654321"
            };
        }

        [Theory]
        [InlineData("example@gmail.com")]
        public async Task RemoveUser_WhenUserExist_DeleteUserAndReturnTrue(string email)
        {
            //Arrange

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var response = await repository.RemoveUserAsync(email);

            //Assert
            Assert.True(response);

        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task RemoveUser_WhenUserDoesntExistOrDeletedFail_ReturnFalse(bool userExist)
        {
            //Arrange
            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(userExist ? user : null);
            _mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult(IdentityResult.Failed()));

            //Act

            var response = await repository.RemoveUserAsync("example@gmail.com");

            //Assert
            Assert.False(response);
        }
    }
}
