using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cobro_Matricula_EPN.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<IdentityRole>().HasData(
        //        new IdentityRole()
        //        {
        //            Name = "Admin",
        //            ConcurrencyStamp = "1",
        //            NormalizedName = "ADMIN",
        //        },
        //        new IdentityRole()
        //        {
        //            Name = "Assistant",
        //            ConcurrencyStamp = "2",
        //            NormalizedName = "ASSISTANT",
        //        }
        //    );

        //}
    }
}
