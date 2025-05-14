using AutoMapper;
using Entity.DTO.User;
using Entity.Entities;

namespace Cobro_Matricula_EPN.Mapping
{
    public class MappingConfig : Profile
    {
        //Mapper para los usuarios
        public MappingConfig()
        {
            //CreateMap<User, UserDto>();
            //CreateMap<UserDto, User>();

            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
