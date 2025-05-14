using AutoMapper;
using Cobro_Matricula_EPN.Context;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.User;
using Entity.Entities;

using Microsoft.AspNetCore.Identity;

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

        public Repository(ApplicationDbContext db, IMapper mapper) 
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
