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

        public DbSet<TreatmentPlanEntity> TreatmentPlans { get; set; }
        //public DbSet<PatientDetailsEntity> Patients { get; set; }
        public DbSet<IPTreatmentPackageEntity> IPTreatmentPackages { get; set; }
        public DbSet<SpecialistEntity> Specialists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IPTreatmentPackageEntity>()
                .Property(i => i.AilmentCategory)
                .HasConversion(
                v => v.ToString(),
                v => (AilmentDomain)Enum.Parse(typeof(AilmentDomain), v));

            modelBuilder.Entity<SpecialistEntity>()
                .Property(s => s.AreaOfExpertise)
                .HasConversion(
                v => v.ToString(),
                v => (AilmentDomain)Enum.Parse(typeof(AilmentDomain), v));
        }
    }
}
