using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.Entites;

namespace IPTreatmentManagement.Models.Dtos.Response
{
    public class InitiateCliamResponseDto
    {
        public int Id { get; set; }

        [Display(Name = "Patient Name Id")]
        public int PatientNameId { get; set; }

        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        public AilmentDomain Ailment { get; set; }

        [Display(Name = "Treatment Package Name")]
        public string TreatmentPackageName { get; set; }

        [Display(Name = "Insurer Package Name")]
        public string InsurerPackageName { get; set; }

        [Display(Name = "Amount ToBe Paid")]
        public decimal AmountToBePaid { get; set; }
    }
}
