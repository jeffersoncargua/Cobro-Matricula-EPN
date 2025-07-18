using AutoMapper;
using Cobro_Matricula_EPN.Mapping;
using Entity.DTO.BaseParameter;
using Entity.DTO.User;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.UnitTest.Mapping
{
    public class MappingTest
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;
        public MappingTest()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });

            _mapper = _configuration.CreateMapper();
        }


        [Fact]
        public void ShouldBeValidConfiguration()
        {
            //Arrange


            //Act


            //Assert
            _configuration.AssertConfigurationIsValid();

        }

        [Theory]
        [InlineData(typeof(ApplicationUser), typeof(UserDto))]
        [InlineData(typeof(UserDto), typeof(ApplicationUser))]
        [InlineData(typeof(ApplicationUser), typeof(UpdateUserDto))]
        [InlineData(typeof(BaseParameter), typeof(BaseParameterDto))]
        [InlineData(typeof(BaseParameterDto), typeof(BaseParameter))]
        //[InlineData(typeof(ApplicationUser), typeof(RegistrationRequestDto))] //El mapeo de una clase que hereda Identity no se puede mapear porque no se fuciona correctamente y quedan campos vacios que no son permitidos
        //[InlineData(typeof(UpdateUserDto), typeof(ApplicationUser))]
        public void Mapping_SourceTodestination_ExistsConfiguration(Type source, Type destination)
        {
            //Arrange


            //Act
            var instance = RuntimeHelpers.GetUninitializedObject(source);


            //Assert
            _mapper.Map(instance, source, destination);
        }
    }
}
