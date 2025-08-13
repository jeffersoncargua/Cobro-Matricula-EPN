using Entity.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.UnitTest.DTOs.User
{
    public class ResetTest : BaseTest
    {
        [Theory]
        [InlineData(null, null, "userAdmin1!", null, 4)]
        [InlineData("hola", "", "userAdmin1!", null, 4)]
        [InlineData("hola@gmail", "userAdmin1", "userAdmin1!", null, 4)]
        [InlineData("hola@gmail", "userAdmin1", "userAdmin1", null, 3)]
        [InlineData("hola@gmail.com", "userAdmin1!", "userAdmin1!", "Aqui va el token", 0)]
        public void ValidDto_ResetRequestDto_ReturnCorrectNumberOfErrors(string email, string password, string confirmPass, string token, int errorsExpected)
        {
            //Arrange
            ResetPasswordRequestDto resetPasswordRequestDto = new()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPass,
                Token = token,
            };

            //Act
            var validationResults = ValidateModel(resetPasswordRequestDto);

            //Assert
            Assert.Equal(errorsExpected, validationResults.Count);
        }
    }
}
