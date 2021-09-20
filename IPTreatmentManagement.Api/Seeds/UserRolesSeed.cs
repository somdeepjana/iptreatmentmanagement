using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPTreatmentManagement.Api.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace IPTreatmentManagement.Api.Seeds
{
    public static class UserRolesSeed
    {
        public static void SeedUserRoles(this RoleManager<IdentityRole> roleManager)
        {
            var roleNames = Enum.GetNames(typeof(UserRoles)).ToList();
            foreach (var role in roleNames)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                    roleManager.CreateAsync(new IdentityRole(role)).Wait();
            }
        }
    }
}
