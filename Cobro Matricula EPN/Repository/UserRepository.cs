using AutoMapper;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.User;
using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
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
        private string secretKey;
        private readonly FrontEndConfig _frontConfig;
        public UserRepository(IMapper mapper,IEmailRepository emailRepo ,IConfiguration config,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, FrontEndConfig frontConfig)
        {
            _mapper = mapper;
            _emailRepo = emailRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            this.secretKey = config.GetValue<string>("APISettings:SecretKey");
            _frontConfig = frontConfig;
            
        }

        /// <summary>
        /// Esta funcion permite verificar el token que se envia al usuario para confirmar su cuenta
        /// </summary>
        /// <param name="token">Es un string generado con Identity GenerateToken en el Register que permite verificar la cuenta de un usuario</param>
        /// <param name="email">Es el correo del usuario que se registro en la plataforma y con el cual se va a verificar con el token </param>
        /// <returns></returns>
        public async Task<bool> ConfirmEmailAsync(string? token = null, string? email = null)
        {
            var userExist = await _userManager.FindByEmailAsync(email);
            if (userExist == null)
            {
                return false;
            }


            if (string.IsNullOrEmpty(token)) 
            {
                return false;
            }

            //Nota: se debe reemplazar los espacios por '+' en blanco ya que al recibir el token se cambian estos caracteres y luego no pueden ser reconocidos para validar el token
            token = token.Replace(" ","+");
            var result = await _userManager.ConfirmEmailAsync(userExist, token);
            if (result.Succeeded)
            {
                return true;
            }

            return false;

        }

        public async Task<ForgetResponseDto> ForgetPasswordAsyn(string email)
        {
            var userExist = await _userManager.FindByEmailAsync(email);
            if (userExist == null)
            {
                return new ForgetResponseDto() { 
                    Success = false,
                    Message = "El usuario no se encuentra registrado",
                    Token = null
                };
            }

            var isConfirmEmail = await IsConfirmEmail(email);
            if (!isConfirmEmail)
            {
                return new ForgetResponseDto()
                {
                    Success = false,
                    Message = "El usuario no ha confirmado su cuenta. Por favor revise su correo para verificar la cuenta",
                    Token = null
                };
            }


            var token = await _userManager.GeneratePasswordResetTokenAsync(userExist);

            var forgetLink = $"{_frontConfig.Url}/manage/reset?token={token}&email={email}";

            var emailMessage = new Message([userExist.Email!], "Recuperacion de Contraseña", $"Para cambiar tu contraseña presiona este <a href='{forgetLink}'>enlace</a> ");

            _emailRepo.SendEmail(emailMessage);

            return new ForgetResponseDto()
            {
                Success = true,
                Message = "Solicitud aceptada. Por favor revice su correo para continuar con el proceso de cambio de contraseña",
                Token = token
            };
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

        private async Task<bool> IsUnique(string email)
        {
            //var result = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            //Permite obtener el usuario a partir del email
            ApplicationUser result = await _userManager.FindByEmailAsync(email);
            if (result == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        { 
            try
            {
                var userExist = await _userManager.FindByEmailAsync(loginRequestDto.Email);
                if (userExist == null)
                {
                    return new LoginResponseDto
                    {
                        User = null,
                        Token = null,
                        Message = "El usuario no esta registrado o el correo es incorrecto"
                    };
                }

                var isConfirmEmail = await IsConfirmEmail(loginRequestDto.Email);
                if (!isConfirmEmail)
                {
                    return new LoginResponseDto
                    {
                        User = null,
                        Token = null,
                        Message = "El usuario no ha verificado su cuenta. Revise su correo para confirmar su cuenta antes de realizar el login"
                    };
                }

                //var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email && u.Password == loginRequestDto.Password);
                var userIsValid = await _userManager.CheckPasswordAsync(userExist, loginRequestDto.Password);
                if (!userIsValid)
                {
                    return new LoginResponseDto
                    {
                        User = null,
                        Token = null,
                        Message = "La contraseña esta incorrecta"
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
                    new(ClaimTypes.Role, roles.FirstOrDefault()) //es posible que se produzca una excepcion si no existe un rol asigando al usuario
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
                    Message = "Login exitoso"
                };

                return response;
            }
            catch (Exception ex)
            {
                //La excepcion se puede presentar si el rol de usuario no esta registrado en la base de datos
                //producto de la migracion, por favor revise la base de datos para verificar que los datos esten correctos
                return new LoginResponseDto
                {
                    User = null,
                    Token = null,
                    Message = "Error. Ha ocurrido un error mientras se realizaba la operación."
                };
            }            
        }

        public async Task<RegisterResponseDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            try
            {
                //var registration = _mapper.Map<User>(registrationRequestDto);

                bool isUnique = await IsUnique(registrationRequestDto.Email);
                if (!isUnique) 
                {
                    return new RegisterResponseDto()
                    {
                        Success = false,
                        MessageResponse = ["Ya existe un registro con ese correo"],
                        Token = null,
                    };
                }

                if (string.IsNullOrEmpty(registrationRequestDto.Role))
                {
                    registrationRequestDto.Role = "Assistant";
                }

                var roleExist = await _roleManager.RoleExistsAsync(registrationRequestDto.Role);
                if (roleExist)
                {

                    //ApplicationUser no se puede mapear ya que no es permitido que queden campos vacios 
                    ApplicationUser user = new()
                    {
                        Name = registrationRequestDto.Name,
                        LastName = registrationRequestDto.LastName,
                        UserName = registrationRequestDto.Email,
                        Email = registrationRequestDto.Email,
                        City = registrationRequestDto.City,
                        Phone = registrationRequestDto.Phone,
                        NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                        //PasswordHash = registrationRequestDto.Password,
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                    
                    if (!result.Succeeded)
                    {
                        List<string> errorsIdentity = new();
                        foreach (var error in result.Errors)
                        {
                            errorsIdentity.Add(error.Description);
                        }
                        return new RegisterResponseDto()
                        {
                            Success = false,
                            MessageResponse = errorsIdentity,
                            Token = null
                        };
                    }

                    await _userManager.AddToRoleAsync(user, registrationRequestDto.Role);

                    string tokenEmail = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    string confirmedEmail = $"{_frontConfig.Url}/manage/confirmation?token={tokenEmail}&email={user.Email}";

                    Message emailMessage = new([user.Email], "Verificación del correo electrónico", $"Para confirmar presiona el <a href='{confirmedEmail}'>enlace</a> ");

                    _emailRepo.SendEmail(emailMessage);

                    return new RegisterResponseDto
                    {
                        Success = true,
                        MessageResponse= new List<string>() {"Registro Exitoso"},
                        Token = tokenEmail
                    };
                }

                return new RegisterResponseDto
                {
                    Success = false,
                    MessageResponse = new List<string>() { "No existe el rol" },
                    Token = null
                };

            }
            catch (Exception ex)
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    MessageResponse = ["Se ha lanzado una excepcion"] 
                };
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

        public async Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordRequestDto resetPasswordRequestDto)
        {
            var userExist = await _userManager.FindByEmailAsync(resetPasswordRequestDto.Email);
            if (userExist == null)
            {
                return new ResetPasswordResponseDto() { 
                    Success = false,
                    Message = "El usuario no se encuentra registrado"
                };
            }

            resetPasswordRequestDto.Token = resetPasswordRequestDto.Token.Replace(" ","+");

            var result = await _userManager.ResetPasswordAsync(userExist,resetPasswordRequestDto.Token,resetPasswordRequestDto.Password);
            if (!result.Succeeded)
            {
                return new ResetPasswordResponseDto()
                {
                    Success = false,
                    Message = "Ha ocurrido un error al cambiar su contraseña. Intentelo nuevamente"
                };
            }

            return new ResetPasswordResponseDto()
            {
                Success = true,
                Message = "Se ha actualizado su contraseña. Por favor, intente iniciar sesion"
            };
        }

        public async Task<UpdateUserResponseDto> UpdateUserAsync(UpdateUserDto updateUserDto,string email)
        {
            var userUpdated = await _userManager.FindByEmailAsync(updateUserDto.Email);
            if(userUpdated == null)
            {
                return new UpdateUserResponseDto () { Message = "El usuario no existe", User = null};
            }

            //Se debe actualizar los campos del objeto ApplicationUser para poder actualizar la informacion
            //del usuario para que no de errores con el metodo UpdateAsync de Identity
            userUpdated.Name = updateUserDto.Name;
            userUpdated.LastName = updateUserDto.LastName;
            userUpdated.City = updateUserDto.City;
            userUpdated.Phone = updateUserDto.Phone;

            if (email != updateUserDto.Email)
            {
                return new UpdateUserResponseDto()
                { 
                    Success = false,
                    Message = "Ha ocurrido un error. No se pudo actualizar el usuario",
                    User = null 
                };
            }

            var result = await _userManager.UpdateAsync(userUpdated);

            if (result.Succeeded)
            {
                //return _mapper.Map<UserDto>(userUpdated);
                return new UpdateUserResponseDto () 
                { 
                    Success = true,
                    Message = "Su información ha sido actualizada", 
                    User = _mapper.Map<UserDto>(userUpdated) 
                };

            }

            return new UpdateUserResponseDto() 
            {
                Success = false,
                Message = "Ha ocurrido un error en el servidor. No se pudo actualizar el usuario" ,
                User = null
            };

        }

        public async Task<List<UserDto>> GetUsers(Expression<Func<ApplicationUser, bool>>? filter = null)
        {
            
            var users = await _userManager.Users.Where(filter).ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
