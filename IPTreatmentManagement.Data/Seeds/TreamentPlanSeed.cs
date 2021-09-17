using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.EFCore.Data.Seeds
{
    public static class TreatmentPlanSeedExtension
    {
        public static bool SeedTreatmentPlan(this ApplicationDbContext context, bool forceCreate = false)
        {
            if (context.TreatmentPlans.Any() && !forceCreate)
                return false;

            foreach (var treatmentPlans in new TreamentPlanSeedData().GetAll)
            {
                context.TreatmentPlans.Add(treatmentPlans);
            }
            return context.SaveChanges() > 0;
        }
    }
    public class TreamentPlanSeedData
    {
        public IEnumerable<TreatmentPlanEntity> GetAll
        {
            get
            {
                return _treatmentPlans;
            }
        }

        private IEnumerable<TreatmentPlanEntity> _treatmentPlans = new List<TreatmentPlanEntity>
        {
            new TreatmentPlanEntity()
            {
                PatientEntityId = 1,
                IPTreatmentPackageEntityId = 1,
                SpecialistEntityId = 1,
                TreatmentCommencementDate = DateTime.Now
            },
            new TreatmentPlanEntity()
            {
                PatientEntityId = 2,
                IPTreatmentPackageEntityId = 3,
                SpecialistEntityId = 3,
                TreatmentCommencementDate = DateTime.Now.AddDays(1)
            }
        };
    }
}
