using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cobro_Matricula_EPN.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
