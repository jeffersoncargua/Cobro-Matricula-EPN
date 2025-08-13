using AutoMapper;
using Cobro_Matricula_EPN.Mapping;
using Cobro_Matricula_EPN.Repository;
using Entity.DTO.BaseParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.IntegratedTest.Repositories.BaseParameterRepositoryTest
{
    [Collection("My Collection")]
    
    public class UpdateBaseParametersTest 
    {
        private SharedDatabaseFixture Fixture { get; }
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public UpdateBaseParametersTest(SharedDatabaseFixture fixture)
        {
            Fixture = fixture;
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Theory]
        [InlineData(1, "Ingeniería 1", 10000f, 1000, 100, 0.2f, 0.8f, 0.1f, 0.05f, 0.15f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f)]
        public async Task UpdatedBaseParameter_WhenSendCorrectUpdatedRequest_ReturnSuccessEqualTrue(int id, string formacionAcademica, float costoOptimo, int horaPeriodoaAcademico, int creditoPeriodoAcademico, float porcentajeCostoOptimoAnual, float porcentajeValorMin, float porcentajeValorMax, float porcentajeValorArancel, float porcentajePromedioAcademico, float porcentajePerdidaTemporal, float porcentajeMatriculaExtraordinario, float porcentajeMatriculaEspecial, float porcentajeRecargoSegunda, float porcentajeRecargoTercera)
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                //Arrange
                UpdatedBaseParameterRequestDto requestDto = new()
                {
                    Id = id,
                    FormacionAcademica = formacionAcademica,
                    CostoOptimo = costoOptimo,
                    HoraPeriodoAcademico = horaPeriodoaAcademico,
                    CreditoPeriodoAcademico = creditoPeriodoAcademico,
                    PorcentajeCostoOptimoAnual = porcentajeCostoOptimoAnual,
                    PorcentajeValorMin = porcentajeValorMin,
                    PorcentajeValorMax = porcentajeValorMax,
                    PorcentajeValorArancel = porcentajeValorArancel,
                    PorcentajePromedioAcademico = porcentajePromedioAcademico,
                    PorcentajePerdidaTemporal = porcentajePerdidaTemporal,
                    PorcentajeMatriculaEspecial = porcentajeMatriculaEspecial,
                    PorcentajeMatriculaExtraordinario = porcentajeMatriculaExtraordinario,
                    PorcentajeRecargoSegunda = porcentajeRecargoSegunda,
                    PorcentajeRecargoTercera = porcentajeRecargoTercera
                };

                using (var context = Fixture.CreateContext(transaction))
                {
                    //Arrange

                    var repository = new BaseParameterRepository(context, _mapper);

                    //Act 

                    var response = await repository.UpdateAsync(id, requestDto);

                    //Assert
                    Assert.True(response.Success);
                    Assert.NotNull(response.Result);
                    Assert.Equal("Los Parametros han sido actualizados correctamente.", response.Message);

                }
            }
        }


        [Theory]
        [InlineData(5, "Ingeniería 1", 10000f, 1000, 100, 0.2f, 0.8f, 0.1f, 0.05f, 0.15f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f)]
        [InlineData(23, "Ingeniería 2", 10001f, 1000, 100, 0.2f, 0.8f, 0.1f, 0.05f, 0.15f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f)]
        public async Task UpdatedBaseParameter_WhenSendInvalidParameterIdRequest_ReturnSuccesEqualFalse(int id, string formacionAcademica, float costoOptimo, int horaPeriodoaAcademico, int creditoPeriodoAcademico, float porcentajeCostoOptimoAnual, float porcentajeValorMin, float porcentajeValorMax, float porcentajeValorArancel, float porcentajePromedioAcademico, float porcentajePerdidaTemporal, float porcentajeMatriculaExtraordinario, float porcentajeMatriculaEspecial, float porcentajeRecargoSegunda, float porcentajeRecargoTercera)
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                //Arrange
                UpdatedBaseParameterRequestDto requestDto = new()
                {
                    Id = id,
                    FormacionAcademica = formacionAcademica,
                    CostoOptimo = costoOptimo,
                    HoraPeriodoAcademico = horaPeriodoaAcademico,
                    CreditoPeriodoAcademico = creditoPeriodoAcademico,
                    PorcentajeCostoOptimoAnual = porcentajeCostoOptimoAnual,
                    PorcentajeValorMin = porcentajeValorMin,
                    PorcentajeValorMax = porcentajeValorMax,
                    PorcentajeValorArancel = porcentajeValorArancel,
                    PorcentajePromedioAcademico = porcentajePromedioAcademico,
                    PorcentajePerdidaTemporal = porcentajePerdidaTemporal,
                    PorcentajeMatriculaEspecial = porcentajeMatriculaEspecial,
                    PorcentajeMatriculaExtraordinario = porcentajeMatriculaExtraordinario,
                    PorcentajeRecargoSegunda = porcentajeRecargoSegunda,
                    PorcentajeRecargoTercera = porcentajeRecargoTercera
                };

                using (var context = Fixture.CreateContext(transaction))
                {
                    //Act 
                    var repository = new BaseParameterRepository(context, _mapper);

                    var response = await repository.UpdateAsync(id, requestDto);


                    //Assert
                    Assert.False(response.Success);
                    Assert.Null(response.Result);
                    Assert.Equal("No se encontró la información de los parametros a actualizar.", response.Message);
                }
            }
        }

        [Fact]
        public async Task UpdatedBaseParameter_WhenSendBaseParameterNull_ReturnSuccessEqualFalse()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                //Arrange
                UpdatedBaseParameterRequestDto requestDto = null;

                using (var context = Fixture.CreateContext(transaction))
                {

                    //Act 
                    var repository = new BaseParameterRepository(context, _mapper);
                    var response = await repository.UpdateAsync(1, requestDto);


                    //Assert
                    Assert.False(response.Success);
                    Assert.Null(response.Result);
                    Assert.Equal("Ha ocurrido un error al buscar la información de los parametros a actualizar.", response.Message);
                }


            }

        }

        [Theory]
        [InlineData(1, "Ingeniería 1", null, null, null, 0.2f, 0.8f, 0.1f, 0.05f, 0.15f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f)]
        [InlineData(1, null, 10000f, 1000, 100, 0.2f, 0.8f, 0.1f, 0.05f, 0.15f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f)]
        public async Task UpdatedBaseParameter_ShouldThrowExceptionWhenSendNullValues_ReturnSuccessEqualFalse(int id, string formacionAcademica, float costoOptimo, int horaPeriodoaAcademico, int creditoPeriodoAcademico, float porcentajeCostoOptimoAnual, float porcentajeValorMin, float porcentajeValorMax, float porcentajeValorArancel, float porcentajePromedioAcademico, float porcentajePerdidaTemporal, float porcentajeMatriculaExtraordinario, float porcentajeMatriculaEspecial, float porcentajeRecargoSegunda, float porcentajeRecargoTercera)
        {
            
            using (var transaction = Fixture.Connection.BeginTransaction()) 
            {
                //Arrange
                UpdatedBaseParameterRequestDto requestDto = new()
                {
                    Id = id,
                    FormacionAcademica = formacionAcademica,
                    CostoOptimo = costoOptimo,
                    HoraPeriodoAcademico = horaPeriodoaAcademico,
                    CreditoPeriodoAcademico = creditoPeriodoAcademico,
                    PorcentajeCostoOptimoAnual = porcentajeCostoOptimoAnual,
                    PorcentajeValorMin = porcentajeValorMin,
                    PorcentajeValorMax = porcentajeValorMax,
                    PorcentajeValorArancel = porcentajeValorArancel,
                    PorcentajePromedioAcademico = porcentajePromedioAcademico,
                    PorcentajePerdidaTemporal = porcentajePerdidaTemporal,
                    PorcentajeMatriculaEspecial = porcentajeMatriculaEspecial,
                    PorcentajeMatriculaExtraordinario = porcentajeMatriculaExtraordinario,
                    PorcentajeRecargoSegunda = porcentajeRecargoSegunda,
                    PorcentajeRecargoTercera = porcentajeRecargoTercera
                };

                using (var context = Fixture.CreateContext(transaction))
                {
                    //Act
                    var repository = new BaseParameterRepository(context, _mapper);

                    var response = await repository.UpdateAsync(id, requestDto);

                    //Assert
                    Assert.Equal("Ha ocurrido un error al actualizar la información de los parametros.", response.Message);
                }
            }
        }
    }
}
