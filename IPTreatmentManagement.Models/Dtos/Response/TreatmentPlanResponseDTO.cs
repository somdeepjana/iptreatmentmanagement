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
        [Required]
        public string PackageName { get; set; }
        [Required]
        public string TestDetails { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public string Specialist { get; set; }
        [Required]
        public DateTime TreatmentCommencementDate { get; set; }
        [Required]
        public DateTime TreatmentEndDate { get; set; }
    }
}
