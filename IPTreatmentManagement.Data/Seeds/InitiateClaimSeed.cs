using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.Entites;

namespace IPTreatmentManagement.EFCore.Data.Seeds
{
    public static class InitiateClaimSeedExtension
    {
        public static bool SeedClaims(this ApplicationDbContext context, bool forceCreate = false)
        {
            if (context.InitiateClaims.Any() && !forceCreate)
                return false;

            foreach (var claim in new InitiateClaimSeedData().GetAll)
            {
                context.InitiateClaims.Add(claim);
            }

            return context.SaveChanges() > 0;
        }
    }

    public class InitiateClaimSeedData
    {
        public IEnumerable<InitiateClaimEntity> GetAll
        {
            get { return _claims; }
        }

        private IEnumerable<InitiateClaimEntity> _claims = new List<InitiateClaimEntity>
        {
            new InitiateClaimEntity()
            {
                InsurerEntityId = 1,
                TreatmentPlanEntityId = 1
            }
        };
    }
}
