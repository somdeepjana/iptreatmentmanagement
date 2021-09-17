using IPTreatmentManagement.Models.Entites;
using IPTreatmentManagement.Models.RepositorieInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.EFCore.Data.Repositories
{
    public class PatientDetailsRepository : Repository, IPatientDetailsRepository
    {
        public PatientDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async void AddAsync(PatientDetailsEntity patient)
        {
            patient.Id = 0;
            patient.IPTreatmentPackageEntity = null;
            await _context.Patients.AddAsync(patient);
        }
    }
}
