using Entity.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.UnitTest.DTOs.User
{
    public class UserUpdatedTest : BaseTest
    {
        [Theory]
        [InlineData(null,null,null,null,null,5)]
        [InlineData("2","a","","sldkf23","hola",6)]
        [InlineData("Aby", "Samantha", "Quito 593", "098765432", "hola@gmail", 2)]
        [InlineData("Aby", "Samantha", "Quito 593", "0987654321", "hola@gmail.com", 0)]
        public void ValidDto_UserUpdatedtDto_ReturnCorrectNumberOfErrors(string name, string lastName, string city, string phone, string email, int errorsExpected)
        {
            //Arrange
            UpdateUserDto updateUserDto = new()
            {
                Name = name,
                LastName = lastName,
                City = city,
                Phone = phone,
                Email = email,
            };

            //Act
            var validationResults = ValidateModel(updateUserDto);

            //Assert
            Assert.Equal(errorsExpected, validationResults.Count);
        }
    }
}
