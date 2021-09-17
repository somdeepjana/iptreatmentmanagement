﻿using AutoMapper;
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
        private readonly IPatientRepository _patientRepository;
        private readonly ITreatmentPlanRepository _treatmentPlanRepository;
        private readonly IIPTreatmentPackageRepository _iPTreatmentPackageRepository;
        private readonly ISpecialistRepository _specialistRepository;

        public FormulateTreatmentTimetableController(
            ILogger<FormulateTreatmentTimetableController> logger,
            IMapper mapper,
            IPatientRepository patientRepository,
            ITreatmentPlanRepository treatmentPlanRepository,
            IIPTreatmentPackageRepository iPTreatmentPackageRepository,
            ISpecialistRepository specialistRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _patientRepository = patientRepository;
            _treatmentPlanRepository = treatmentPlanRepository;
            _iPTreatmentPackageRepository = iPTreatmentPackageRepository;
            _specialistRepository = specialistRepository;
        }       

        [HttpPost]
        public async Task<ActionResult<TreatmentPlanResponseDTO>> GetTreatmentPlanDetails(PatientRequestDTO patient)
        {
            var patientEntity = _mapper.Map<PatientEntity>(patient);

            var iPTreatmentPackage = await _iPTreatmentPackageRepository.GetByNameAsync(patient.TreatmentPackageName);
            if(iPTreatmentPackage is null)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No IPTreatmentPackage by name '{patient.TreatmentPackageName}' is found ",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int)ApplicationStatusCodes.IPTreatmentPackageEntityNotFound
                };
                //_logger.LogTrace()
                return NotFound(error);
            }

            var specialist = (await _specialistRepository.GetSpecialistByAreaOfExpertseAsync(iPTreatmentPackage.AilmentCategory)).FirstOrDefault();
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
                PatientEntity = patientEntity,
                IPTreatmentPackageEntityId = iPTreatmentPackage.Id,
                //IPTreatmentPackageEntity= iPTreatmentPackage,
                TreatmentCommencementDate = patient.TreatmentCommencementDate,
                SpecialistEntityId = specialist.Id
                //SpecialistEntity = specialist
            };
            await _treatmentPlanRepository.AddAsync(treatmentPlan);
            await _treatmentPlanRepository.SaveChangesAsync();

            return _mapper.Map<TreatmentPlanResponseDTO>(treatmentPlan);
        }
    }
}
