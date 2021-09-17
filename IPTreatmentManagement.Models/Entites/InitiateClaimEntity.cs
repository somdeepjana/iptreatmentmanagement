using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Entites
{
    public class InitiateClaimEntity
    {
        public int PatientEntityId { get; set; }
        public PatientEntity PatientEntity { get; set; }
        public int IPTreatmentPackageEntityId { get; set; }
        public IPTreatmentPackageEntity IPTreatmentPackageEntity { get; set; }
        public int InsurerEntityId { get; set; }
        public InsurerEntity InsurerEntity { get; set; }
    }
}
