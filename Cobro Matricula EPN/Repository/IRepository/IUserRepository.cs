using Entity.DTO.User;

namespace Cobro_Matricula_EPN.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> IsUnique(string email);
        Task<bool> IsConfirmEmail(string email);
        Task<bool> Register(RegistrationRequestDto registrationRequestDto);

        Task<bool> RemoveUserAsync(string email);

        Task<bool> ConfirmEmailAsync(string email, string token);

        Task<bool> ForgetPasswordAsyn(string email);

        Task<bool> ResetPasswordAsync(ResetPasswordRequestDto resetPasswordRequestDto);

        Task<UserDto> UpdateUserAsync(UpdateUserDto updateUserDto);
    }
}
