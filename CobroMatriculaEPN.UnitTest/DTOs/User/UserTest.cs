using Entity.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.UnitTest.DTOs.User
{
    public class UserTest : BaseTest
    {
        [Theory]
        [InlineData(null, null,null,null,null,5)]
        [InlineData("", "", "", "", "", 5)]
        [InlineData("a", "22222", "hola", "", "asdasd", 6)]
        [InlineData("a", "22222", "hola@", null, "asdasd", 6)]
        [InlineData("aby", "medina", "hola@gmail", null, "098765432", 3)]
        [InlineData("aby", "medina", "hola@gmail.com", null, "0987654321", 1)]
        [InlineData("aby", "medina", "hola@gmail.com", "Quito", "0987654321", 0)]
        public void ValidDto_UserDto_ReturnCorrectNumberOfErrors(string name, string lastName, string email, string city, string phone, int errorExpected)
        {
            //Arrange
            UserDto userDto = new UserDto()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                City = city,
                Phone = phone,
            };

            //Act
            var validationResuits = ValidateModel(userDto);

            //Assert
            Assert.Equal(errorExpected, validationResuits.Count);

        }
    }
}
