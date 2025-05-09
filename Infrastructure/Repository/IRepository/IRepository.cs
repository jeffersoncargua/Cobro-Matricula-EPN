using Entity.DTO.User;

namespace Infrastructure.Repository.IRepository
{
    public interface IRepository
    {
        UserDto Login(LoginRequestDto loginRequestDto);
    }
}
