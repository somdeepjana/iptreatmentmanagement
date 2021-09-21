using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Dtos.Request
{
    public class InitiateClaimRequestDto
    {
        [Required]
        [Display(Name = "TreatmentPlan Id")]
        public int TreatmentPlanEntityId { get; set; }

        [Required]
        [Display(Name = "Insurer Package Name")]
        public string InsurerPackageName { get; set; }
    }
}
