using Entity.DTO.User;

namespace Cobro_Matricula_EPN.Repository.IRepository
{
    public interface IRepository
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> IsUnique(string email);

        Task<bool> Register(RegistrationRequestDto registrationRequestDto);

        Task<bool> RemoveAsync(int id);

        Task Save();
    }
}
