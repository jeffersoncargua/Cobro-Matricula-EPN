using AutoMapper;
using Cobro_Matricula_EPN.Context;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.User;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cobro_Matricula_EPN.Repository
{
    public class Repository : IRepository.IRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private string secretKey;
        public Repository(ApplicationDbContext db, IMapper mapper,IConfiguration config)
        {
            _db = db;
            _mapper = mapper;
            secretKey = config.GetValue<string>("APISettings:SecretKey");
        }

        public async Task<bool> IsUnique(string email)
        {
            var result = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (result == null) {
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
            var userExist = await _db.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email);
            if (userExist == null)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = "",
                    Message = "El usuario no se encuentra registrado"
                };
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email && u.Password == loginRequestDto.Password);
            if (user == null)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = "",
                    Message = "La contraseña está incorrecta. Ingresa correctamente la contraseña"
                };
            }

            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role!.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);

            var userDto = _mapper.Map<UserDto>(user);

            LoginResponseDto response = new()
            {
                User = userDto,
                Token = tokenhandler.WriteToken(token),
                Message = "Login Exitoso!!"
            };

            return  response;
        }

        public async Task<bool> Register(RegistrationRequestDto registrationRequestDto)
        {
            try
            {
                var registration = _mapper.Map<User>(registrationRequestDto);
                await _db.Users.AddAsync(registration);
                await Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var userExist = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userExist != null)
            {
                _db.Users.Remove(userExist);
                await Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
