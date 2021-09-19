using IPTreatmentManagement.EFCore.Data;
using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Api.Seeds
{
    public static class IPTreatmentPackageSeedExtension
    {
        public static bool SeedIPTreatmentPackages(this ApplicationDbContext context, bool forceCreate= false)
        {
            if (context.IPTreatmentPackages.Any() && !forceCreate)
                return false;

            foreach(var iPTreatmentPackage in new IPTreatmentPackageSeedData().GetAll)
            {
                if(context.IPTreatmentPackages.FirstOrDefault(
                    i=>i.TreatmentPackageName== iPTreatmentPackage.TreatmentPackageName)==null)
                    context.IPTreatmentPackages.Add(iPTreatmentPackage);
            }

            return context.SaveChanges() > 0;
        }
    }

    public class IPTreatmentPackageSeedData
    {
        public IEnumerable<IPTreatmentPackageEntity> GetAll
        {
            get
            {
                return _iPTreatmentPackages;
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

        private IEnumerable<IPTreatmentPackageEntity> _iPTreatmentPackages = new List<IPTreatmentPackageEntity>
        {
            new IPTreatmentPackageEntity
            {
                TreatmentPackageName= "TRN-1",
                TestDetails= MockTestDetails,
                AilmentCategory= AilmentDomain.Orthopaedics,
                Cost= 2500,
                TreatmentDurationInDays= 4
            },
            new IPTreatmentPackageEntity
            {
                TreatmentPackageName= "TRN-2",
                TestDetails= MockTestDetails,
                AilmentCategory= AilmentDomain.Orthopaedics,
                Cost= 3000,
                TreatmentDurationInDays= 6
            },
            new IPTreatmentPackageEntity
            {
                TreatmentPackageName= "TRN-3",
                TestDetails= MockTestDetails,
                AilmentCategory= AilmentDomain.Urology,
                Cost= 4000,
                TreatmentDurationInDays= 4
            },
            new IPTreatmentPackageEntity
            {
                TreatmentPackageName= "TRN-4",
                TestDetails= MockTestDetails,
                AilmentCategory= AilmentDomain.Urology,
                Cost= 5000,
                TreatmentDurationInDays= 6
            }
        };
    }
}
