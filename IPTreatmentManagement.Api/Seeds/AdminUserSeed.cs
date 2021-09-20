using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPTreatmentManagement.Api.ConfigurationModels;
using IPTreatmentManagement.Api.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace IPTreatmentManagement.Api.Seeds
{
    public static class AdminUserSeed
    {
        public static bool SeedAdminUser(this UserManager<ApplicationUser> userManager,
            AdminCredentialConfiguration adminCredential)
        {
            var existingUser = userManager.FindByEmailAsync(adminCredential.Email).Result;
            if (existingUser != null)
                return false;

            var admin = new ApplicationUser()
            {
                UserName = adminCredential.UserName,
                Email = adminCredential.Email,
                EmailConfirmed = true
            };

            if (userManager.CreateAsync(admin, adminCredential.Password).Result.Succeeded)
            {
                return userManager.AddToRoleAsync(admin, nameof(UserRoles.Admin)).Result.Succeeded;
            }

            return false;
        }
    }
}
