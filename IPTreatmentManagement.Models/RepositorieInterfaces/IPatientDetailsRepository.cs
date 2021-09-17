using System;
using IPTreatmentManagement.Models.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.RepositorieInterfaces
{
    public interface IPatientDetailsRepository
    {
        Task AddAsync(PatientDetailsEntity patient);
    }
}
