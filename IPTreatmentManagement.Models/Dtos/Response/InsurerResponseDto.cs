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
        public string InsurerName { get; set; }
        public string InsurerPackageName { get; set; }
        public decimal InsuranceAmountLimit { get; set; }
        public int DisbursementDurationInDays { get; set; }
    }
}
