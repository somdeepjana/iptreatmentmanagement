using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Dtos.Response
{
    public class TreatmentPlanResponseDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }

        [Required]
        [Display(Name = "Test Details")]
        public string TestDetails { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public string Specialist { get; set; }

        [Required]
        [Display(Name = "Treatment Commencement Date")]
        public DateTime TreatmentCommencementDate { get; set; }

        [Required]
        [Display(Name = "Treatment End Date")]
        public DateTime TreatmentEndDate { get; set; }
    }
}
