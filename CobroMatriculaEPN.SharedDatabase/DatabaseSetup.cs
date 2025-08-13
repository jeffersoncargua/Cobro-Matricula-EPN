using AutoMapper;
using Bogus;
using Cobro_Matricula_EPN.Context;
using Entity.DTO.User;
using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.SharedDatabaseSetup
{
    public class DatabaseSetup
    {
        public static void SeedData(ApplicationDbContext context) 
        {
            //Generar roles de usuario
            context.Roles.RemoveRange(context.Roles);
            context.SaveChanges();

            var roleId1 = Guid.NewGuid().ToString();
            var roleId2 = Guid.NewGuid().ToString();

            var fakeRole1 = new Faker<IdentityRole>()
                .RuleFor(o => o.Id, f => roleId1)
                .RuleFor(o => o.Name, f => "Admin")
                .RuleFor(o => o.NormalizedName, f => "ADMIN")
                .RuleFor(o => o.ConcurrencyStamp, f => "1");

            var fakeRole2 = new Faker<IdentityRole>()
                .RuleFor(o => o.Id, f => roleId2)
                .RuleFor(o => o.Name, f => "Assistant")
                .RuleFor(o => o.NormalizedName, f => "ASSISTANT")
                .RuleFor(o => o.ConcurrencyStamp, f => "2");

            context.Roles.AddRange(fakeRole1, fakeRole2);
            context.SaveChanges();


            //Generar Usuarios

            //context.Users.RemoveRange(context.Users);
            //context.SaveChanges();

            ////var UserIdGuid1 = Guid.NewGuid().ToString();
            ////var UserIdGuid2 = Guid.NewGuid().ToString();

            //var fakeUser1 = new Faker<User>()
            //    .RuleFor(o => o.Id, f => 1)
            //    .RuleFor(o => o.Email, f => $"user1@example.com")
            //    .RuleFor(o => o.Password, f => "UserAmin1!");

            //var fakeUser2 = new Faker<User>()
            //    .RuleFor(o => o.Id, f => 2)
            //    .RuleFor(o => o.Email, f => $"user2@example.com")
            //    .RuleFor(o => o.Password, f => "UserAmin1!");


            //context.Users.AddRange(fakeUser1,fakeUser2);
            //context.SaveChanges();

            //Generar usuarios con roles

            //context.UserRoles.RemoveRange(context.UserRoles);
            //context.SaveChanges();

            //var fakeUserRole1 = new Faker<IdentityUserRole<string>>()
            //    .RuleFor(o => o.RoleId, f => roleId1)
            //    .RuleFor(o => o.UserId, f => "1");

            //var fakeUserRole2 = new Faker<IdentityUserRole<string>>()
            //    .RuleFor(o => o.RoleId, f => roleId2)
            //    .RuleFor(o => o.UserId, f => "2");


            //context.UserRoles.AddRange(fakeUserRole1, fakeUserRole2);
            //context.SaveChanges();

            //Generar Parametros Base para el calculo de matricula

            context.BaseParameters.RemoveRange(context.BaseParameters);
            context.SaveChanges();

            int baseParameterId = 1;
            var fakeBaseParameter = new Faker<BaseParameter>()
                .RuleFor(o => o.Id, f => baseParameterId++)
                .RuleFor(o => o.FormacionAcademica, f => $"Ingeniería {baseParameterId}")
                .RuleFor(o => o.CostoOptimo, f => 3325f)
                .RuleFor(o => o.CostoOptimoPeriodo, f => 3325f * 0.5f)
                .RuleFor(o => o.ValorMin, f => (3325f * 0.5f) * (0.1f))
                .RuleFor(o => o.ValorMatriculaMin, f => (((3325f * 0.5f) * (0.1f)) / 1.1f) * (0.1f))
                .RuleFor(o => o.ValorArancelMin, f => ((3325f * 0.5f) * (0.1f)) / 1.1f)
                .RuleFor(o => o.ValorMax, f => (3325f * 0.5f) * (0.5f))
                .RuleFor(o => o.ValorMatriculaMax, f => (((3325f * 0.5f) * (0.5f)) / 1.1f) * 0.1f)
                .RuleFor(o => o.ValorArancelMax, f => ((3325f * 0.5f) * (0.5f)) / 1.1f)
                .RuleFor(o => o.HoraPeriodoAcademico, f => 15 * 48)
                .RuleFor(o => o.HoraPromedioPeriodoAcademico, f => 0.52f * 15 * 48)
                .RuleFor(o => o.CreditoPeriodoAcademico, f => 15)
                .RuleFor(o => o.CreditoPerdidaTemporal, f => 9)
                .RuleFor(o => o.CostoHoraPeriodo, f => (3325f * 0.5f) / (0.52f * 15 * 48 * 1.1f))
                .RuleFor(o => o.PorcentajeCostoOptimoAnual, f => 0.1f)
                .RuleFor(o => o.PorcentajeValorMin, f => 0.1f)
                .RuleFor(o => o.PorcentajeValorMax, f => 0.5f)
                .RuleFor(o => o.PorcentajeValorArancel, f => 0.1f)
                .RuleFor(o => o.PorcentajePromedioAcademico, f => 0.52f)
                .RuleFor(o => o.PorcentajePerdidaTemporal, f => 0.6f)
                .RuleFor(o => o.PorcentajeMatriculaExtraordinario, f => 0.1f)
                .RuleFor(o => o.PorcentajeMatriculaEspecial, f => 0.25f)
                .RuleFor(o => o.PorcentajeRecargoSegunda, f => 0.25f)
                .RuleFor(o => o.PorcentajeRecargoTercera, f => 0.1f);

            var fakeBaseParameters = fakeBaseParameter.Generate(2);

            context.BaseParameters.AddRange(fakeBaseParameters);
            context.SaveChanges();

        }
    }
}
