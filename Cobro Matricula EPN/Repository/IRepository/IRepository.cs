using Entity.DTO.User;

namespace Cobro_Matricula_EPN.Repository.IRepository
{
    public interface IRepository
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
