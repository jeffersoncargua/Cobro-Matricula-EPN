using AutoMapper;
using Cobro_Matricula_EPN.Mapping;
using Cobro_Matricula_EPN.Repository;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace CobroMatriculaEPN.IntegratedTest.Repositories.UserRepositoryTest
{
    public class UserMockSetup
    {
        public readonly Mock<UserManager<ApplicationUser>> _mockUserManager; //Permite generar un simulador para ejecutar las acciones de UserManager
        public readonly Mock<RoleManager<IdentityRole>> _mockRoleManager; //Permite generar un simulador para ejecutar las acciones de UserManager
        public readonly IMapper _mapper;
        public readonly AutoMapper.IConfigurationProvider _configuration; //Permite configurar las funciones del mapper para poder utilizarlo en las pruebas
        //private readonly EmailConfiguration _emailConfiguration;
        public readonly Mock<IConfigurationSection> _mockConfigurationValue; //Permite simular la obtencion del valor del appSettings.json para generar la clave secreta
        public readonly Mock<IConfiguration> _mockConfiguration; //Permite simular la obtencion de la clave secreta para la realizacion de las pruebas
        public readonly Mock<IEmailRepository> _mockIEmail; //Permite simular las acciones que realiza la interfaz de IEmailRepository para el envio de correos
        public readonly FrontEndConfig _frontEndConfig; //permite obtener el valore de la url del front end para el envio del enlace para la confirmacion de la cuenta
        //private readonly Mock<UserRepository> _mockUserRepository;
        public UserRepository repository;

        public UserMockSetup()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });

            _mapper = _configuration.CreateMapper();

            //Permite instanciar el Mock de UserManager para utilizar los metodos y funciones para la gestion de usuarios a traves de Identity
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object
            );

            //Permite isntancias el Mock de RoleManager para utilizar los metodos y funciones para la gestion de roles de usuario a traves de Identity
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                new IRoleValidator<IdentityRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<IdentityRole>>>().Object
            );

            _frontEndConfig = new FrontEndConfig()
            {
                Url = "https://localhost:3000"
            };

            _mockConfigurationValue = new Mock<IConfigurationSection>();
            _mockConfigurationValue.Setup(x => x.Value).Returns("---- Aqui va la llave secreta ----");

            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(x => x.GetSection("APISettings:SecretKey")).Returns(_mockConfigurationValue.Object);

            //_mockUserRepository = new Mock<UserRepository>();
            _mockIEmail = new Mock<IEmailRepository>();
            //Permite configurar la simulacion del envio de un correo electronico 
            _mockIEmail.Setup(x => x.SendEmail(It.IsAny<Message>()));

            repository = new UserRepository(_mapper, _mockIEmail.Object, _mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _frontEndConfig);
        }

    }
}
