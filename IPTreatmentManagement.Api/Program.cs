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
using IPTreatmentManagement.Api.ConfigurationModels;
using IPTreatmentManagement.Api.Data;
using IPTreatmentManagement.Api.Models.Entity;
using Microsoft.AspNetCore.Identity;

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

                #region Business Data seeded
                if (dbContext.SeedIPTreatmentPackages())
                    logger.LogInformation("IPTreatmentPackages Data seeded into the database");
                if (dbContext.SeedSpecialists())
                    logger.LogInformation("Specialist Data seeded into the database");
                if (dbContext.SeedTreatmentPlan())
                    logger.LogInformation("TreatmentPlan Data seeded into the database");
                if (dbContext.SeedPatientData())
                    logger.LogInformation("Patient Data seeded into the database");
                if (dbContext.SeedInsurers())
                    logger.LogInformation("Insurer Data seeded into the database");
                if (dbContext.SeedClaims())
                    logger.LogInformation("InsuranceClaims Data seeded into the database");
                #endregion

                #region Admin credential seeded
                var identityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();
                identityDbContext.Database.EnsureCreated();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var adminCredentials = services.GetRequiredService<IConfiguration>()
                    .GetSection("AdminCredentials").Get<AdminCredentialConfiguration>();

                roleManager.SeedUserRoles();
                if (userManager.SeedAdminUser(adminCredentials))
                    logger.LogInformation("Admin User Data seeded into the database");
                #endregion
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
