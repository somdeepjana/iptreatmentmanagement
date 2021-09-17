using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Entites
{
    public class PatientEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public AilmentDomain Ailment { get; set; }        
        
        [Required]
        public int IPTreatmentPackageEntityID { get; set; }

        //Navigation Property
        public IPTreatmentPackageEntity IPTreatmentPackageEntity { get; set; }
    }
}