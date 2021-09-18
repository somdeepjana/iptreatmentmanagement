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

        public static string MockTestDetails
        {
            get
            {
                return "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            }
        }

        private IEnumerable<InsurerEntity> _insurers = new List<InsurerEntity>
        {
            new InsurerEntity
            {
                InsurerName = "Insurer 1",
                IPTreatmentPackageEntityId = 1,
                InsuranceAmountLimit = 5000,
                DisbursementDuration = 4
            },
            new InsurerEntity
            {
                InsurerName = "Insurer 2",
                IPTreatmentPackageEntityId = 2,
                InsuranceAmountLimit = 4000,
                DisbursementDuration = 5
            },
            new InsurerEntity
            {
                InsurerName = "Insurer 3",
                IPTreatmentPackageEntityId = 3,
                InsuranceAmountLimit = 6000,
                DisbursementDuration = 6
            },
            new InsurerEntity
            {
                InsurerName = "Insurer 4",
                IPTreatmentPackageEntityId = 4,
                InsuranceAmountLimit = 7000,
                DisbursementDuration = 3
            },

        };
    }
}
