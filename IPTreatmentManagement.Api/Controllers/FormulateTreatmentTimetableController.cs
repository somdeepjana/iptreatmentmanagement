using AutoMapper;
using IPTreatmentManagement.Models.Dtos.Request;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.Entites;
using IPTreatmentManagement.Models.RepositorieInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormulateTreatmentTimetableController : ControllerBase
    {
        private readonly ILogger<FormulateTreatmentTimetableController> _logger;
        private readonly IMapper _mapper;

        public FormulateTreatmentTimetableController(
            ILogger<FormulateTreatmentTimetableController> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }       

        [HttpPost]
        public async Task<ActionResult<TreatmentPlanResponseDTO>> GetTreatmentPlanDetails(PatientServiceRequestDTO patient)
        {
            var patientDetails = _mapper.Map<PatientDetailsEntity>(patient);
            var entity = new TreatmentPlanEntity()
            {
                Id = 100,
                IPTreatmentPackageEntity = new IPTreatmentPackageEntity()
                {
                    TestDetails = "OPT1",
                    Cost = 1000,
                    TreatmentPackageName = "Package1",
                    TreatmentDurationInDays = 3
                },
                TreatmentCommencementDate = DateTime.Now,
                SpecialistEntity = new SpecialistEntity
                {
                    Name = "Specialist 5",
                    AreaOfExpertise = AilmentDomain.Orthopaedics,
                    ExperienceInYears = 5,
                    ContactNumber = "2545879865"
                }
            };
            return _mapper.Map<TreatmentPlanResponseDTO>(entity);
        }

           
    }
}
