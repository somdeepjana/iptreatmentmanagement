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
using Microsoft.AspNetCore.Http;

namespace IPTreatmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentPlansController : ControllerBase
    {
        private readonly ILogger<TreatmentPlansController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public TreatmentPlansController(
            ILogger<TreatmentPlansController> logger,
            IMapper mapper,
            ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<TreatmentPlanResponseDTO[]>> GetAllTreatmentPlans()
        {
            var treatmentPlans = await _context.TreatmentPlans
                .Include(t => t.IPTreatmentPackageEntity)
                .Include(t => t.PatientEntity)
                .Include(t => t.SpecialistEntity).ToListAsync();

            return _mapper.Map<TreatmentPlanResponseDTO[]>(treatmentPlans);
        }

        [HttpPost("FormulateTreatmentTimetable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseModel))]
        public async Task<ActionResult<TreatmentPlanResponseDTO>> GenerateTreatmentPlan(PatientRequestDTO patient)
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
                TreatmentCommencementDate = patient.TreatmentCommencementDate,
                SpecialistEntityId = specialist.Id
            };
            await _context.AddAsync(treatmentPlan);
            await _context.SaveChangesAsync();

            return _mapper.Map<TreatmentPlanResponseDTO>(treatmentPlan);
        }
    }
}
