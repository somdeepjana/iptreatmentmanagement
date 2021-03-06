using AutoMapper;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.Dtos.Request;
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

            CreateMap<TreatmentPlanEntity, TreatmentPlanResponseDTO>()
                .ForMember(td => td.PackageName,
                o => o.MapFrom(te => te.IPTreatmentPackageEntity.TreatmentPackageName))
                .ForMember(td => td.Cost,
                o => o.MapFrom(te => te.IPTreatmentPackageEntity.Cost))
                .ForMember(td => td.TestDetails,
                o => o.MapFrom(td => td.IPTreatmentPackageEntity.TestDetails))
                .ForMember(td => td.Specialist,
                o => o.MapFrom(td => td.SpecialistEntity.Name))
                .ForMember(td => td.TreatmentEndDate,
                o => o.MapFrom(te => te.TreatmentCommencementDate.AddDays(te.IPTreatmentPackageEntity.TreatmentDurationInDays)));


            CreateMap<PatientRequestDTO, PatientEntity>();

            CreateMap<InsurerEntity, InsurerResponseDto>();

            CreateMap<InitiateClaimEntity, InitiateCliamResponseDto>()
                .ForMember(id => id.PatientNameId, o => o.MapFrom(ie => ie.TreatmentPlanEntity.PatientEntityId))
                .ForMember(id => id.PatientName, o => o.MapFrom(ie => ie.TreatmentPlanEntity.PatientEntity.Name))
                .ForMember(id => id.Ailment,
                    o => o.MapFrom(ie => ie.TreatmentPlanEntity.IPTreatmentPackageEntity.AilmentCategory))
                .ForMember(id => id.TreatmentPackageName,
                    o => o.MapFrom(ie => ie.TreatmentPlanEntity.IPTreatmentPackageEntity.TreatmentPackageName))
                .ForMember(id => id.InsurerPackageName, o => o.MapFrom(ie => ie.InsurerEntity.InsurerPackageName))
                .ForMember(id => id.AmountToBePaid,
                    o => o.MapFrom(ie => ie.TreatmentPlanEntity.IPTreatmentPackageEntity.Cost));
        }
    }
}