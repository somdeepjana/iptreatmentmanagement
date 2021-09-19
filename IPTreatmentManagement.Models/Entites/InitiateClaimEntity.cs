using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Entites
{
    public class InitiateClaimEntity
    {
        [Key]
        public int Id { get; set; }

        public int TreatmentPlanEntityId { get; set; }
        public TreatmentPlanEntity TreatmentPlanEntity { get; set; }

        public int InsurerEntityId { get; set; }
        public InsurerEntity InsurerEntity { get; set; }
    }
}
