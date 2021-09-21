using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Dtos.Response
{
    public class SpecialistResponseDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Area Of Expertise")]
        public AilmentDomain AreaOfExpertise { get; set; }

        [Required]
        [Display(Name = "Experience In Years")]
        public int ExperienceInYears { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
    }
}
