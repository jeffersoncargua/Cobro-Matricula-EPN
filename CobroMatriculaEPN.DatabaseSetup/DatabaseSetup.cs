using Bogus;
using Cobro_Matricula_EPN.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.DatabaseSetup
{
    public class DatabaseSetup
    {
        public static void SeedData(ApplicationDbContext context)
        {
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
        }
    }
}
