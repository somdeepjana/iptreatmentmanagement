using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Dtos.Response
{
    public class IPTreatmentPackageResponseDto
    {
        [Required]
        [Display(Name = "Treatment Package Name")]
        public string TreatmentPackageName { get; set; }

        [Required]
        [Display(Name = "Test Details")]
        public string TestDetails { get; set; }

        [Required]
        [Display(Name = "Ailment Category")]
        public AilmentDomain AilmentCategory { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        [Display(Name = "Treatment Duration in Days")]
        public int TreatmentDurationInDays { get; set; }
    }
}
