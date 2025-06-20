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

            var fakeRole1 = new Faker<IdentityRole>()
                .RuleFor(o => o.Id, f => Guid.NewGuid().ToString())
                .RuleFor(o => o.Name, f => "Admin")
                .RuleFor(o => o.NormalizedName, f => "ADMIN")
                .RuleFor(o => o.ConcurrencyStamp, f => "1");

            var fakeRole2 = new Faker<IdentityRole>()
                .RuleFor(o => o.Id, f => Guid.NewGuid().ToString())
                .RuleFor(o => o.Name, f => "Assistant")
                .RuleFor(o => o.NormalizedName, f => "ASSISTANT")
                .RuleFor(o => o.ConcurrencyStamp, f => "2");

            context.Roles.AddRange(fakeRole1, fakeRole2);
            context.SaveChanges();




            //Generar Usuarios




        }
    }
}
