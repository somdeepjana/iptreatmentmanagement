using IPTreatmentManagement.Models.Entites;
using IPTreatmentManagement.Models.RepositorieInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.EFCore.Data.Repositories
{
    public class PatientRepository : Repository, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PatientEntity patient)
        {
            patient.Id = 0;
            patient.IPTreatmentPackageEntity = null;
            await _context.Patients.AddAsync(patient);
        }
    }
}
