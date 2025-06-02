using Entity.DTO.User;
using Entity.Entities;
using System.Linq.Expressions;

namespace Cobro_Matricula_EPN.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        //Task<bool> IsUnique(string email);
        //Task<bool> IsConfirmEmail(string email);
        Task<RegisterResponseDto> Register(RegistrationRequestDto registrationRequestDto);

        Task<bool> RemoveUserAsync(string email);

        Task<bool> ConfirmEmailAsync(string email, string token);

        Task<ForgetResponseDto> ForgetPasswordAsyn(string email);

        Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordRequestDto resetPasswordRequestDto);

        Task<UserDto> UpdateUserAsync(UpdateUserDto updateUserDto);

        //Task<bool> UserExist(string email);

        Task<List<UserDto>> GetUsers(Expression<Func<ApplicationUser, bool>>? filter = null);
    }
}
