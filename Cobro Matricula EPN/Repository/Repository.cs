using AutoMapper;
using Cobro_Matricula_EPN.Context;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.User;
using Microsoft.AspNetCore.Mvc.Filters;
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
    }
}
