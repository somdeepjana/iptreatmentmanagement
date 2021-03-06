using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Entites
{
    public class SpecialistEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public AilmentDomain AreaOfExpertise { get; set; }

        [Required]
        public int ExperienceInYears { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }
    }
}
