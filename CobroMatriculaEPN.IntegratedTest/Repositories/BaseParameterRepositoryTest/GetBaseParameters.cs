

using AutoMapper;
using Cobro_Matricula_EPN.Mapping;
using Cobro_Matricula_EPN.Repository;

namespace CobroMatriculaEPN.IntegratedTest.Repositories.BaseParameterRepositoryTest
{
    
    
    
    [Collection("My Collection")]
    public class GetBaseParameters
    {
        private SharedDatabaseFixture Fixture { get; }
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public GetBaseParameters( SharedDatabaseFixture fixture)
        {
            Fixture = fixture;
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });
            _mapper = _configuration.CreateMapper();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetBaseParameters_WhenSendCorrectRequest_ReturnParameterObject(int id)
        {
            using (var context = Fixture.CreateContext())
            {
                //Arrange 

                var repository = new BaseParameterRepository(context, _mapper);

                //Act
                var result = await repository.GetAsync(u => u.Id == id);

                //Assert
                Assert.NotNull(result);
            }
        }

        [Theory]
        [InlineData(18)]
        [InlineData(56)]
        public async Task GetBaseParameters_WhenParametersIdDoesntExist_ReturnNullObject(int id)
        {
            using (var context = Fixture.CreateContext())
            {
                //Arrange 

                var repository = new BaseParameterRepository(context, _mapper);

                //Act
                var result = await repository.GetAsync(u => u.Id == id);

                //Assert
                Assert.Null(result);
            }
        }
    }
}
