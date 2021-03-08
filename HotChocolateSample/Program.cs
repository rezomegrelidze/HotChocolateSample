using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateSample.Core;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolateSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<CompanyDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    internal class DbInitializer
    {
        public static void Initialize(CompanyDbContext context)
        {
            context.Database.EnsureCreated();

            if(context.Companies.Any()) return;
            context.Companies.AddRange(new []
            {
                new Company(){Name = "McDonalds",Revenue = 213212},
                new Company(){Name = "Apple",Revenue = 213215},
                new Company(){Name = "H&O",Revenue = 213216},
                new Company(){Name = "Google",Revenue = 213217},
            });
            context.SaveChanges();
        }
    }
}
