using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Entites
{
    public class InsurerEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string InsurerName { get; set; }
        [Required]
        public string InsurerPackageName { get; set; }
        [Required]
        public decimal InsuranceAmountLimit { get; set; }
        [Required]
        public int DisbursementDurationInDays { get; set; }
    }
}
