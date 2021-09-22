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
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Area Of Expertise")]
        public AilmentDomain AreaOfExpertise { get; set; }

        [Display(Name = "Experience In Years")]
        public int ExperienceInYears { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
    }
}
