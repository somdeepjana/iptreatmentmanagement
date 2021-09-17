using AutoMapper;
using IPTreatmentManagement.Models.Dtos.Request;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.Entites;
using IPTreatmentManagement.Models.OperationalModels;
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
        private readonly IPatientDetailsRepository _patientDetailsRepository;
        private readonly ITreatmentPlanRepository _treatmentPlanRepository;
        private readonly IIPTreatmentPackageRepository _iPTreatmentPackageRepository;
        private readonly ISpecialistRepository _specialistRepository;

        public FormulateTreatmentTimetableController(
            ILogger<FormulateTreatmentTimetableController> logger,
            IMapper mapper,
            IPatientDetailsRepository patientDetailsRepository,
            ITreatmentPlanRepository treatmentPlanRepository,
            IIPTreatmentPackageRepository iPTreatmentPackageRepository,
            ISpecialistRepository specialistRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _patientDetailsRepository = patientDetailsRepository;
            _treatmentPlanRepository = treatmentPlanRepository;
            _iPTreatmentPackageRepository = iPTreatmentPackageRepository;
            _specialistRepository = specialistRepository;
        }       

        [HttpPost]
        public async Task<ActionResult<TreatmentPlanResponseDTO>> GetTreatmentPlanDetails(PatientServiceRequestDTO patient)
        {
            var patientEntity = _mapper.Map<PatientDetailsEntity>(patient);
            var iPTreatmentPackage = await _iPTreatmentPackageRepository.GetByNameAsync(patient.TreatmentPackageName);
            if(iPTreatmentPackage is null)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No IPTreatmentPackage by name '{patient.TreatmentPackageName}' found",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int)ApplicationStatusCodes.IPTreatmentPackageEntityNotFound
                };
                //_logger.LogTrace()
                return NotFound(error);
            }
            patientEntity.IPTreatmentPackageEntityID = iPTreatmentPackage.Id;
            await _patientDetailsRepository.AddAsync(patientEntity);           

            var specialist = (await _specialistRepository.GetSpecialistByAreaOfExpertseAsync(patient.Ailment)).FirstOrDefault();
            if(specialist is null)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No Specialist Found!",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int)ApplicationStatusCodes.SpecialistEntityNotFound
                };
                //_logger.LogTrace()
                return NotFound(error);
            }
            var treatmentPlan = new TreatmentPlanEntity()
            {
                IPTreatmentPackageEntityId = iPTreatmentPackage.Id,
                TreatmentCommencementDate = patient.TreatmentCommencementDate,
                SpecialistEntityId = specialist.Id,
            };
            await _treatmentPlanRepository.AddAsync(treatmentPlan);
            //await _treatmentPlanRepository.SaveChangesAsync();
            var treatmentPlanToReturn = _treatmentPlanRepository.GetTreatmentPlanAsync(treatmentPlan.Id);           


            return _mapper.Map<TreatmentPlanResponseDTO>(treatmentPlanToReturn);
        }
    }
}
