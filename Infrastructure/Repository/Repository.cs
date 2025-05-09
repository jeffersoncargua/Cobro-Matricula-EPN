using Cobro_Matricula_EPN.Context;
using Entity.DTO.User;
using Infrastructure.Repository.IRepository;

namespace Infrastructure.Repository
{
    public class Repository : IRepository.IRepository
    {
        private readonly ApplicationDbContext _db;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }

        public UserDto Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
