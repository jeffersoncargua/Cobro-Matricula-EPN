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
    public class UserRepositoryUpdatedUserTest : UserMockSetup
    {
        [Theory]
        [InlineData("newName", "newLastName", "newCity", "0987654321", "newEmail@gmail.com")]
        public async Task UpdatedUser_WhenSendCorrectRequest_ReturnUserDto(string name, string lastName, string city, string phone, string email)
        {
            //Arrange
            UpdateUserDto updateUserDto = new()
            {
                Email = email,
                LastName = lastName,
                Name = name,
                City = city,
                Phone = phone,
            };

            ApplicationUser user = new()
            {
                Email = email,
                LastName = lastName,
                Name = name,
                City = city,
                Phone = phone,
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var response = await repository.UpdateUserAsync(updateUserDto, email);

            //Assert
            Assert.IsType<UpdateUserResponseDto>(response);
        }

        [Theory]
        [InlineData("newName", "newLastName", "newCity", "0987654321", "newEmail@gmail.com", false)]
        [InlineData("newName", "newLastName", "newCity", "0987654321", "newEmail@gmail.com", true)]
        public async Task UpdateUser_WhenUserDoesntExistOrUpdatedFail(string name, string lastName, string city, string phone, string email, bool userExist)
        {
            //Arrange
            UpdateUserDto updateUserDto = new()
            {
                Email = email,
                LastName = lastName,
                Name = name,
                City = city,
                Phone = phone,
            };

            ApplicationUser user = new()
            {
                Email = email,
                LastName = lastName,
                Name = name,
                City = city,
                Phone = phone,
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(userExist ? user : null);
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult(IdentityResult.Failed()));

            //Act
            var response = await repository.UpdateUserAsync(updateUserDto, email);

            //Assert
            Assert.Null(response.User);
        }
    }
}
