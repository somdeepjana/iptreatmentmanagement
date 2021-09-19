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
        public string TreatmentPlanEntityId { get; set; }
        [Required]
        public string InsurerPackageName { get; set; }
    }
}
