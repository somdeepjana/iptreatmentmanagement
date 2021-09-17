using AutoMapper;
using IPTreatmentManagement.Models.Dtos.Request;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.Entites;
using IPTreatmentManagement.Models.OperationalModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using IPTreatmentManagement.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace IPTreatmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormulateTreatmentTimetableController : ControllerBase
    {
        private readonly ILogger<FormulateTreatmentTimetableController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public FormulateTreatmentTimetableController(
            ILogger<FormulateTreatmentTimetableController> logger,
            IMapper mapper,
            ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }       

        [HttpPost]
        public async Task<ActionResult<TreatmentPlanResponseDTO>> GetTreatmentPlanDetails(PatientRequestDTO patient)
        {
            var patientEntity = _mapper.Map<PatientEntity>(patient);

            var iPTreatmentPackage = await _context.IPTreatmentPackages
                .FirstOrDefaultAsync(i=>i.TreatmentPackageName== patient.TreatmentPackageName);
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

            //var specialist = (await _specialistRepository.GetSpecialistByAreaOfExpertseAsync(iPTreatmentPackage.AilmentCategory)).FirstOrDefault();
            var specialist = await _context.Specialists
                .Where(s => s.AreaOfExpertise == iPTreatmentPackage.AilmentCategory).FirstOrDefaultAsync();
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
            //await _treatmentPlanRepository.AddAsync(treatmentPlan);
            await _context.AddAsync(treatmentPlan);
            await _context.SaveChangesAsync();

            return _mapper.Map<TreatmentPlanResponseDTO>(treatmentPlan);
        }
    }
}
