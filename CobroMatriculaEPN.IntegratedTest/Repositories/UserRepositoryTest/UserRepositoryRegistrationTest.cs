using AutoMapper;
using Bogus.DataSets;
using Cobro_Matricula_EPN.Mapping;
using Cobro_Matricula_EPN.Repository;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.User;
using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Numerics;
using Utility;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace CobroMatriculaEPN.IntegratedTest.Repositories.UserRepositoryTest
{
    public class UserRepositoryRegistrationTest : UserMockSetup
    {

        [Theory]
        [InlineData("Jeff", "Cargua", "jeffersoncargua@gmail.com", "Quito", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assitant", "Registro Exitoso", true)]
        public async Task RegistrationUser_WhenSendCorrectRegistrationRequestDto(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, string expectedMessage, bool expectedResponse)
        {
            //Arrange
            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                City = city,
                Phone = phone,
                Password = password,
                ConfirmPass = confirmPass,
                Role = role
            };

            //Permite simular la consulta de un rol que debe estar almacenada en la base de datos y retornar un verdadero
            _mockRoleManager.Setup(x => x.RoleExistsAsync(role)).ReturnsAsync(true);
            //Permite simular la el registro de un usuario en la base de datos y retornar un una respuesta afirmativa de la accion de creacion
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            //Permite simular la asignacion del rol de usuario 
            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
            //Permite simular la generacion de un token de confirmacion para el usuario que se ha registrado correctamente y retornar el token generado
            _mockUserManager.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(It.IsAny<string>());

            //Act
            var response = await repository.Register(registrationRequestDto);

            //Assert
            Assert.Equal(expectedResponse, response.Success);
            Assert.Equal(expectedMessage, response.MessageResponse[0]);
        }


        [Theory]
        [InlineData("Jeff", "Cargua", "jeffersoncargua@gmail.com", "Quito", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assitant", "Ya existe un registro con ese correo", true, true)]
        [InlineData("Jeff", "Cargua", "jeffersoncargua@gmail.com", "Quito", "0987654321", "UserAdmin1!", "UserAdmin1!", "Costumer", "No existe el rol", false, false)]
        [InlineData("Jeff", "Cargua", "jeffersoncargua@gmail.com", "Quito", "0987654321", "UserAdmin1!", "UserAdmin1!", "Assitant", "Se ha lanzado una excepcion", false, true)]
        public async Task RegistrationUser_WhenUserExistOrRoleDoesntExist_ReturnSuccessFalse(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, string expectedMessage, bool userExist, bool expectedRole)
        {
            //Arrange
            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                City = city,
                Phone = phone,
                Password = password,
                ConfirmPass = confirmPass,
                Role = role
            };

            ApplicationUser user = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                Phone = phone,
                City = city
            };

            //Permite simular si el usuario existe o no y retorna un ApplicationUser o null
            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(userExist ? user : null);
            //Permite simular que el rol no existe y retorna un false
            _mockRoleManager.Setup(x => x.RoleExistsAsync(role)).ReturnsAsync(expectedRole);

            //Act
            var response = await repository.Register(registrationRequestDto);


            //Assert
            Assert.False(response.Success);
            Assert.Equal(expectedMessage, response.MessageResponse[0]);

        }


        [Theory]
        [InlineData("Jeff", "Cargua", "jeffersoncargua@gmail.com", "Quito", "0987654321", "UserAdmin1", "UserAdmin1", "Assitant", "Se ha lanzado una excepcion", false, true)]
        public async Task RegistrationUser_WhenSendIncorrectRegister_ReturnSuccessFalse(string name, string lastName, string email, string city, string phone, string password, string confirmPass, string role, string expectedMessage, bool userExist, bool expectedRole)
        {
            //Arrange
            RegistrationRequestDto registrationRequestDto = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                City = city,
                Phone = phone,
                Password = password,
                ConfirmPass = confirmPass,
                Role = role
            };

            ApplicationUser user = new()
            {
                Name = name,
                LastName = lastName,
                Email = email,
                Phone = phone,
                City = city
            };

            var identityErrors = new IdentityError[]
            {
                new ()
                {
                    Code = "Contraseña inválida",
                    Description = "Contraseña inválida"
                }
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(userExist ? user : null);
            _mockRoleManager.Setup(x => x.RoleExistsAsync(role)).ReturnsAsync(expectedRole);
            //Permite simular el metodo CreateAsync y se retorna un Success = Failed y el errorIdentity que es el codigo y descripcion del error que se produjo
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(identityErrors)));

            //Act
            var response = await repository.Register(registrationRequestDto);


            //Assert
            Assert.False(response.Success);
        }
    }
}
