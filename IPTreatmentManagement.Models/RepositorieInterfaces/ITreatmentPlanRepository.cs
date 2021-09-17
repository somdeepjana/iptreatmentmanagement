using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.RepositorieInterfaces
{
    public interface ITreatmentPlanRepository : IRepository
    {
        Task<TreatmentPlanEntity> GetTreatmentTimeTableAsync(PatientDetailsEntity patient);
    }
}
