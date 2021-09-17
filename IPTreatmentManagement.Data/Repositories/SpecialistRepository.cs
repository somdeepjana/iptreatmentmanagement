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

        public async Task<IEnumerable<SpecialistEntity>> GetSpecialistByAreaOfExpertseAsync(AilmentDomain ailment)
        {
            return await _context.Specialists.Where(x => x.AreaOfExpertise == ailment).ToListAsync();
        }

        public async Task<SpecialistEntity> GetSpecialistByNameAsync(string specialistName, AilmentDomain ailment)
        {
            return await _context.Specialists.FirstOrDefaultAsync(x => x.Name == specialistName && x.AreaOfExpertise == ailment);
        }
    }
}
