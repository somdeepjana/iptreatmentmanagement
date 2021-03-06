using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.EFCore.Data.Seeds
{
    public static class InsurerSeedExtension
    {
        public static bool SeedInsurers(this ApplicationDbContext context, bool forceCreate = false)
        {
            if (context.Insurers.Any() && !forceCreate)
                return false;

            foreach (var insurer in new InsurerSeedData().GetAll)
            {
                if (context.Insurers.FirstOrDefault(
                    i => i.InsurerPackageName == insurer.InsurerPackageName) == null)
                    context.Insurers.Add(insurer);
            }

            return context.SaveChanges() > 0;
        }
    }

    public class InsurerSeedData
    {
        public IEnumerable<InsurerEntity> GetAll
        {
            get
            {
                return _insurers;
            }
        }

        private IEnumerable<InsurerEntity> _insurers = new List<InsurerEntity>
        {
            new InsurerEntity
            {
                InsurerName = "Insurer 1",
                InsurerPackageName = "I1P1",
                InsuranceAmountLimit = 5000,
                DisbursementDurationInDays = 4
            },
            new InsurerEntity
            {
                InsurerName = "Insurer 2",
                InsurerPackageName = "I2P1",
                InsuranceAmountLimit = 4000,
                DisbursementDurationInDays = 5
            },
            new InsurerEntity
            {
                InsurerName = "Insurer 3",
                InsurerPackageName = "I3P3",
                InsuranceAmountLimit = 6000,
                DisbursementDurationInDays = 6
            },
            new InsurerEntity
            {
                InsurerName = "Insurer 4",
                InsurerPackageName = "I4P2",
                InsuranceAmountLimit = 7000,
                DisbursementDurationInDays = 3
            },

        };
    }
}
