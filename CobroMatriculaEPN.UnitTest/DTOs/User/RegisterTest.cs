using Entity.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.UnitTest.DTOs.User
{
    public class RegisterTest : BaseTest
    {
        [Theory]
        [InlineData("q","2","hola","Quito 593","iuax","userAdmin1", "userAdmin2",null,7)]
        [InlineData("q", "2", "hola@gmail", "Quito 593", "iuax", "userAdmin1", "userAdmin2",null, 6)]
        [InlineData("holas2", "hih", "hola@", "", "098765432w", "useradmin1", "useradmin1", "Asistant", 6)]
        [InlineData("juan", "medina", "hola@gmail", "Quito", "0987654321", "UserAdmin1!", "UserAdmin1!", "Asistant", 1)]
        [InlineData("Juan", "Medina", "hola@gmail.com", "Quito 593", "0987654321", "userAdmin1!", "userAdmin1!", "Asistant", 0)]
        public void ValidDto_RegisterRequestDto_ReturnCorrectNumberOfErrors(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, int errorsExpected)
        {
            //Arrange
            RegistrationRequestDto registrationRequestDto = new()
            {
                Name= name,
                LastName= lastName,
                Email= email,
                City= city,
                Phone = phone,
                Password= password,
                ConfirmPass= confirmPass,
                Role=role,
            };

            //Act

            var validationResults = ValidateModel(registrationRequestDto);


            //Assert

            Assert.Equal(errorsExpected, validationResults.Count);
        }
    }
}
