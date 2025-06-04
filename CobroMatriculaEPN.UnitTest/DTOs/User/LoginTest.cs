using Entity.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.UnitTest.DTOs.User
{
    public class LoginTest : BaseTest
    {
        [Theory]
        [InlineData("Hola@", "", 3)]
        [InlineData("Hola@gmail","juan12",1)]
        [InlineData("Hola@gmail", "", 2)]
        [InlineData("Hola@gmail.", "Juan1234!", 1)]
        [InlineData("Hola@gmail.net", "Juan1234!", 0)]
        public void ValidDto_LoginRequestDto_ReturnCorrectNumberOfErrors(string email, string password, int errorExpected)
        {
            //Arrage
            LoginRequestDto loginRequestDto = new()
            {
                Email = email,
                Password = password,
            };

            //Act
            var errorResult = ValidateModel(loginRequestDto);


            //Assert
            Assert.Equal(errorExpected, errorResult.Count);
        }
    }
}
