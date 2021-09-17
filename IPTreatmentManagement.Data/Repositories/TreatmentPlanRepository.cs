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
    public class TreatmentPlanRepository : Repository, ITreatmentPlanRepository
    {
        public TreatmentPlanRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task AddAsync(TreatmentPlanEntity treatmentPlan)
        {
            treatmentPlan.Id = 0;
            treatmentPlan.IPTreatmentPackageEntity = null;
            treatmentPlan.SpecialistEntity = null;
            await _context.TreatmentPlans.AddAsync(treatmentPlan);
        }

        public async Task<TreatmentPlanEntity> GetTreatmentPlanAsync(int treatmentPlanId)
        {            
            return await _context.TreatmentPlans.Include(x => x.IPTreatmentPackageEntity)
                .Include(x => x.SpecialistEntity)
                .FirstOrDefaultAsync(x => x.Id == treatmentPlanId);                                                
        }
    }
}
