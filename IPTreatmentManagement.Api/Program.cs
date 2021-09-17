using IPTreatmentManagement.Api.Seeds;
using IPTreatmentManagement.EFCore.Data;
using IPTreatmentManagement.EFCore.Data.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<Program>>();
                var env = services.GetRequiredService<IWebHostEnvironment>();
                var dbContext = services.GetRequiredService<ApplicationDbContext>();

                dbContext.Database.EnsureCreated();

                if (dbContext.SeedIPTreatmentPackages())
                    logger.LogInformation("IPTreatmentPackages Data seeded into the database");
                if (dbContext.SeedSpecialists())
                    logger.LogInformation("Specialist Data seeded into the database");
                if (dbContext.SeedTreatmentPlan())
                    logger.LogInformation("TreatmentPlan Data seeded into the database");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
