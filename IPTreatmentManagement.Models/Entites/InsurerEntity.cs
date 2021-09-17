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
        public int Id { get; set; }
        [Required]
        public string InsurerName { get; set; }
        public int IPTreatmentPackageEntityId { get; set; }
        public IPTreatmentPackageEntity IPTreatmentPackageEntity { get; set; }
        [Required]
        public int InsurerAmountLimit { get; set; }
        [Required]
        public int DisbursementDuration { get; set; }
    }
}
