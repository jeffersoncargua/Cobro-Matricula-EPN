using Castle.Core.Logging;
using Cobro_Matricula_EPN.Context;
using CobroMatriculaEPN.SharedDatabaseSetup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.FunctionalTest
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForFunctionalTesting");
                });

                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var storeDbContext = scopedServices.GetRequiredService<ApplicationDbContext>();
                    storeDbContext.Database.EnsureCreated();

                    try
                    {
                        DatabaseSetup.SeedData(storeDbContext);
                    }
                    catch (Exception ex)
                    {

                        logger.LogError(ex, $"An error occurred seeding the Store database with test messages. Error: {ex.Message}");
                    }

                }
            });

        }

        public void CustomConfigureServices(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopeServices = scope.ServiceProvider;

                    var logger = scopeServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var storeDbContext = scopeServices.GetRequiredService<ApplicationDbContext>();

                    try
                    {
                        DatabaseSetup.SeedData(storeDbContext);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the Store database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
