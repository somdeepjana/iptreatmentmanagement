using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.OperationalModels
{
    public enum ApplicationStatusCodes
    {
        IPTreatmentPackageEntityNotFound,
        SpecialistEntityNotFound,
        InsurerEntityNotFound,
        TreatmentPlanEntityNotFound,
        ExceedInsuraceClaimAmount,
        ApplicationUserNotFound,
        UserCredentialMismatch
    }
}
