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
        [Display(Name = "Treatment Package Name")]
        public string TreatmentPackageName { get; set; }

        [Display(Name = "Test Details")]
        public string TestDetails { get; set; }

        [Display(Name = "Ailment Category")]
        public AilmentDomain AilmentCategory { get; set; }

        public decimal Cost { get; set; }

        [Display(Name = "Treatment Duration in Days")]
        public int TreatmentDurationInDays { get; set; }
    }
}
