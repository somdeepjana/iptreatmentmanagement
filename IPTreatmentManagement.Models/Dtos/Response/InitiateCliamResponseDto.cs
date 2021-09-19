using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.Entites;

namespace IPTreatmentManagement.Models.Dtos.Response
{
    public class InitiateCliamResponseDto
    {
        public int Id { get; set; }
        public int PatientNameId { get; set; }
        public string PatientName { get; set; }
        public AilmentDomain Ailment { get; set; }
        public string TreatmentPackageName { get; set; }
        public string InsurerPackageName { get; set; }
        public decimal AmountToBePaid { get; set; }
    }
}
