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

        [Display(Name = "Package Name")]
        public string PackageName { get; set; }

        [Display(Name = "Test Details")]
        public string TestDetails { get; set; }

        public int Cost { get; set; }

        public string Specialist { get; set; }

        [Display(Name = "Treatment Commencement Date")]
        public DateTime TreatmentCommencementDate { get; set; }

        [Display(Name = "Treatment End Date")]
        public DateTime TreatmentEndDate { get; set; }
    }
}
