using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Entites
{
    public class TreatmentPlanEntity
    {
        [Key]
        public int Id { get; set; }        
        [Required]
        public DateTime TreatmentCommencementDate { get; set; }

        public int PatientEntityId { get; set; }
        public PatientEntity PatientEntity { get; set; }

        public int IPTreatmentPackageEntityId { get; set; }
        public IPTreatmentPackageEntity IPTreatmentPackageEntity { get; set; }

        public int SpecialistEntityId { get; set; }
        public SpecialistEntity SpecialistEntity { get; set; }
    }
}