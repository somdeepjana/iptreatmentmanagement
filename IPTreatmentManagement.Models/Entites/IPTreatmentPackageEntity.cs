using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Entites
{
    public class IPTreatmentPackageEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TreatmentPackageName { get; set; }

        [Required]
        public string TestDetails { get; set; }

        [Required]
        public AilmentDomain AilmentCategory { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public int TreatmentDurationInDays { get; set; }

    }
}
