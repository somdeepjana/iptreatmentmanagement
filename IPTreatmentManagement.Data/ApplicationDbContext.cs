using IPTreatmentManagement.Models.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.EFCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<IPTreatmentPackageEntity> IPTreatmentPackages { get; set; }
        public DbSet<SpecialistEntity> Specialists { get; set; }

    }
}
