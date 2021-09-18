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
        [Required]
        public int Id { get; set; }
        [Required]
        public string InsurerName { get; set; }
        [Required]
        public string InsurerPackageName { get; set; }
        [Required]
        public int InsuranceAmountLimit { get; set; }
        [Required]
        public int DisbursementDuration { get; set; }
    }
}
