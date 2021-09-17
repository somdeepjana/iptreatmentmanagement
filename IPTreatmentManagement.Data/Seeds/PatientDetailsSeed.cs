using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.EFCore.Data.Seeds
{
    public static class PatientDetailsSeedExtension
    {
        public static bool SeedPatientData(this ApplicationDbContext context, bool forceCreate = false)
        {
            if (context.Patients.Any() && !forceCreate)
                return false;

            foreach (var patient in new PatientDetailsSeed().GetAll)
            {                
                context.Patients.Add(patient);
            }
            return context.SaveChanges() > 0;
        }
    }
    public class PatientDetailsSeed
    {
        public IEnumerable<PatientDetailsEntity> GetAll
        {
            get
            {
                return _patientDetails;
            }
        }

        private IEnumerable<PatientDetailsEntity> _patientDetails = new List<PatientDetailsEntity>
        {
            new PatientDetailsEntity()
            {
                Name = "Alex",
                Age = 30,
                Ailment = AilmentDomain.Orthopaedics,
                IPTreatmentPackageEntityID = 1
            },
            new PatientDetailsEntity()
            {
                Name = "Brian",
                Age = 41,
                Ailment = AilmentDomain.Urology,
                IPTreatmentPackageEntityID = 2
            }
        };
    }
}

