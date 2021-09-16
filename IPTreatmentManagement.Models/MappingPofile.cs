using AutoMapper;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models
{
    public class MappingPofile : Profile
    {
        public MappingPofile()
        {
            CreateMap<IPTreatmentPackageEntity, IPTreatmentPackageResponseDto>();

            CreateMap<SpecialistEntity, SpecialistResponseDto>();
        }
    }
}
