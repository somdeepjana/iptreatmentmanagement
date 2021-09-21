using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Dtos.Request
{
    public class PatientRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        //[Required]
        //public AilmentDomain Ailment { get; set; }
        [Required]
        [Display(Name = "Treatment Package Name")]
        public string TreatmentPackageName { get; set; }

        [Required]
        [Display(Name = "Treatment Commencement Date")]
        public DateTime TreatmentCommencementDate { get; set; }
    }
}
