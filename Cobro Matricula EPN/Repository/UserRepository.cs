using AutoMapper;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.User;
using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utility;

namespace Cobro_Matricula_EPN.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly IEmailRepository _emailRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string secretKey;

        public UserRepository(IMapper mapper,IEmailRepository emailRepo ,IConfiguration config,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _emailRepo = emailRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            secretKey = config.GetValue<string>("APISettings:SecretKey")!;
        }

        public async Task<bool> ConfirmEmailAsync(string token, string email)
        {
            var userExist = await _userManager.FindByEmailAsync(email);
            if (userExist == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(userExist, token);
            if (result.Succeeded)
            {
                return true;
            }

            return false;

        }

        public async Task<bool> ForgetPasswordAsyn(string email)
        {
            var userExist = await _userManager.FindByEmailAsync(email);
            if (userExist == null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(userExist);

            var forgetLink = $"{"Aqui va la url del la pagina del url para recuperar contraseña"}?token={token}&email={email}";

            var emailMessage = new Message([userExist.Email!], "Recuperacion de Contraseña", $"Para cambiar tu contraseña presiona este <a href='{forgetLink}'>enlace</a> ");

            _emailRepo.SendEmail(emailMessage);

            return true;
        }

        public async Task<bool> IsConfirmEmail(string email)
        {
            var userExist = await _userManager.FindByEmailAsync(email);
            if (userExist == null) 
            {
                return false;
            }

            return await _userManager.IsEmailConfirmedAsync(userExist);
        }

        public async Task<bool> IsUnique(string email)
        {
            //var result = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            //Permite obtener el usuario a partir del email
            var result = await _userManager.FindByEmailAsync(email);
            if (result == null)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            //var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email && u.Password == loginRequestDto.Password);
            //var userExist = await _db.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email);
            var userExist = await _userManager.FindByEmailAsync(loginRequestDto.Email);
            if (userExist == null)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = ""
                };
            }

            //var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email && u.Password == loginRequestDto.Password);
            var userIsValid = await _userManager.CheckPasswordAsync(userExist, loginRequestDto.Password);
            if (!userIsValid)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = "",
                };
            }

            var roles = await _userManager.GetRolesAsync(userExist);
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new(ClaimTypes.Name, userExist.Email!.ToString()),
                    new(ClaimTypes.Role, roles.FirstOrDefault())
                ]),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenhandler.CreateToken(tokenDescriptor);

            var userDto = _mapper.Map<UserDto>(userExist);

            LoginResponseDto response = new()
            {
                User = userDto,
                Token = tokenhandler.WriteToken(token),
            };

            return response;
        }

        public async Task<bool> Register(RegistrationRequestDto registrationRequestDto)
        {
            try
            {
                //var registration = _mapper.Map<User>(registrationRequestDto);

                if (await _roleManager.RoleExistsAsync(registrationRequestDto.Role))
                {
                    ApplicationUser user = new()
                    {
                        Name = registrationRequestDto.Name,
                        LastName = registrationRequestDto.LastName,
                        UserName = registrationRequestDto.Email,
                        Email = registrationRequestDto.Email,
                        City = registrationRequestDto.City,
                        Phone = registrationRequestDto.Phone,
                        NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                        PasswordHash = registrationRequestDto.Password,
                    };

                    var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

                    if (!result.Succeeded)
                    {
                        return false;
                    }

                    await _userManager.AddToRoleAsync(user, registrationRequestDto.Role);

                    var tokenEmail = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmedEmail = $"{"Aqui va la url del front-end"}?token={tokenEmail}&email={user.Email}";

                    var emailMessage = new Message([user.Email], "Verificación del correo electrónico", $"Para confirmar presiona el <a href='{confirmedEmail}'>enlace</a> ");

                    _emailRepo.SendEmail(emailMessage);

                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }

        public async Task<bool> RemoveUserAsync(string email)
        {
            //var userExist = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            var userExist = await _userManager.FindByEmailAsync(email);
            if (userExist != null)
            {
                //_db.Users.Remove(userExist);
                var result = await _userManager.DeleteAsync(userExist);
                //await Save();
                if (result.Succeeded)
                {
                    return true;
                }

                return false;

            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto resetPasswordRequestDto)
        {
            var userExist = await _userManager.FindByEmailAsync(resetPasswordRequestDto.Email);
            if (userExist == null)
            {
                return false;
            }

            var result = await _userManager.ResetPasswordAsync(userExist,resetPasswordRequestDto.Token,resetPasswordRequestDto.Password);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<UserDto> UpdateUserAsync(UserDto user)
        {
            var userExist = await _userManager.FindByEmailAsync(user.Email);
            if(userExist == null)
            {
                return null;
            }

            ApplicationUser userUpdated = _mapper.Map<ApplicationUser>(userExist);

            userUpdated.Name= user.Name;
            userUpdated.LastName = user.LastName;
            userUpdated.City = user.City;
            userUpdated.Phone = user.Phone;


            var result = await _userManager.UpdateAsync(userUpdated);

            if (result.Succeeded)
            {
                return user;
            }
            
            return null;

        }
    }
}
