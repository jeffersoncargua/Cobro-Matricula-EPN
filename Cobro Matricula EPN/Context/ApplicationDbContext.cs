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
        public DbSet<BaseParameter> BaseParameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Name = "Admin",
                    ConcurrencyStamp = "1",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole()
                {
                    Name = "Assistant",
                    ConcurrencyStamp = "2",
                    NormalizedName = "ASSISTANT",
                }
            );

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaseParameter>().HasData(

                new BaseParameter
                {
                    Id = 1,
                    FormacionAcademica = "Ingeniería",
                    CostoOptimo = 3325f,
                    CostoOptimoPeriodo = 3325f * 0.5f,
                    ValorMin = (3325f * 0.5f) * (0.1f),
                    ValorMatriculaMin = (((3325f * 0.5f) * (0.1f)) / 1.1f) * (0.1f),
                    ValorArancelMin = ((3325f * 0.5f) * (0.1f))/1.1f,
                    ValorMax = (3325f * 0.5f) * (0.5f),
                    ValorMatriculaMax = (((3325f * 0.5f) * (0.5f)) / 1.1f) * 0.1f,
                    ValorArancelMax = ((3325f * 0.5f) * (0.5f)) / 1.1f,
                    HoraPeriodoAcademico = 15 * 48,
                    HoraPromedioPeriodoAcademico = 0.52f * 15 * 48,
                    CreditoPeriodoAcademico = 15,
                    CreditoPerdidaTemporal = 9,
                    CostoHoraPeriodo = (3325f * 0.5f)/ (0.52f * 15 * 48 * 1.1f),
                    PorcentajeCostoOptimoAnual = 0.1f,
                    PorcentajeValorMin = 0.1f,
                    PorcentajeValorMax = 0.5f,
                    PorcentajeValorArancel = 0.1f,
                    PorcentajePromedioAcademico = 0.52f,
                    PorcentajePerdidaTemporal = 0.6f,
                    PorcentajeMatriculaExtraordinario = 0.25f,
                    PorcentajeMatriculaEspecial = 0.25f,
                    PorcentajeRecargoSegunda = 0.1f,
                    PorcentajeRecargoTercera = 0.21f
                },
                new BaseParameter
                {
                    Id = 2,
                    FormacionAcademica = "Tecnología",
                    CostoOptimo = 3325f,
                    CostoOptimoPeriodo = 3325f * 0.5f,
                    ValorMin = (3325f * 0.5f) * (0.1f),
                    ValorMatriculaMin = (((3325f * 0.5f) * (0.1f)) / 1.1f) * (0.1f),
                    ValorArancelMin = ((3325f * 0.5f) * (0.1f)) / 1.1f,
                    ValorMax = (3325f * 0.5f) * (0.5f),
                    ValorMatriculaMax = (((3325f * 0.5f) * (0.5f)) / 1.1f) * 0.1f,
                    ValorArancelMax = ((3325f * 0.5f) * (0.5f)) / 1.1f,
                    HoraPeriodoAcademico = 15 * 48,
                    HoraPromedioPeriodoAcademico = 0.52f * 15 * 48,
                    CreditoPeriodoAcademico = 15,
                    CreditoPerdidaTemporal = 5,
                    CostoHoraPeriodo = (3325f * 0.5f) / (0.52f * 15 * 48 * 1.1f),
                    PorcentajeCostoOptimoAnual = 0.1f,
                    PorcentajeValorMin = 0.1f,
                    PorcentajeValorMax = 0.5f,
                    PorcentajeValorArancel = 0.1f,
                    PorcentajePromedioAcademico = 0.56f,
                    PorcentajePerdidaTemporal = 0.6f,
                    PorcentajeMatriculaExtraordinario = 0.25f,
                    PorcentajeMatriculaEspecial = 0.25f,
                    PorcentajeRecargoSegunda = 0.1f,
                    PorcentajeRecargoTercera = 0.21f
                }

                );
        }
    }
}
