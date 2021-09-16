using IPTreatmentManagement.Models.Entites;
using IPTreatmentManagement.Models.RepositorieInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.EFCore.Data.Repositories
{
    public class IPTreatmentPackageRepository : Repository, IIPTreatmentPackageRepository
    {
        public IPTreatmentPackageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<IPTreatmentPackageEntity>> GetAllAsync()
        {
            return await _context.IPTreatmentPackages.ToListAsync();
        }

        public async Task<IPTreatmentPackageEntity> GetByNameAsync(string packageName)
        {
            return await _context.IPTreatmentPackages.FirstOrDefaultAsync(i => i.TreatmentPackageName == packageName);
        }
    }
}
