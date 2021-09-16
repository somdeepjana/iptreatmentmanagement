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
    public class SpecialistRepository : Repository, ISpecialistRepository
    {
        public SpecialistRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SpecialistEntity>> GetAllAsync()
        {
            return await _context.Specialists.ToListAsync();
        }
    }
}
