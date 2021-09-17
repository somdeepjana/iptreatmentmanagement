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

        public Task<TreatmentPlanEntity> GetTreatmentTimeTableAsync(PatientDetailsEntity patient)
        {
            throw new NotImplementedException();
        }
        //public Task<TreatmentPlanEntity> GetTreatmentTimeTableAsync(PatientDetailsEntity patient)
        //{

        //    var packageDetail = _context.IPTreatmentPackages
        //                                .Where(x => x.AilmentCategory.Equals(patient.Ailment) && 
        //                                x.TreatmentPackageName == patient.TreatmentPackageName).FirstOrDefault();
        //}
    }
}
