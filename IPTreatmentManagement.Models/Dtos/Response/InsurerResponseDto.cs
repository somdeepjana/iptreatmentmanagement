using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Dtos.Response
{
    public class InsurerResponseDto
    {
        [Display(Name = "Insurer Name")]
        public string InsurerName { get; set; }

        [Display(Name = "Insurer Package Name")]
        public string InsurerPackageName { get; set; }

        [Display(Name = "Insurance Amount Limit")]
        public decimal InsuranceAmountLimit { get; set; }

        [Display(Name = "Disbursement Duration In Days")]
        public int DisbursementDurationInDays { get; set; }
    }
}
