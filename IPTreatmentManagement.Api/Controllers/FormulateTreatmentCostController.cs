using AutoMapper;
using IPTreatmentManagement.EFCore.Data;
using IPTreatmentManagement.Models.Dtos.Request;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.Entites;
using IPTreatmentManagement.Models.OperationalModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ClaimsController(ILogger<ClaimsController> logger,
            IMapper mapper,
            ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<InitiateCliamResponseDto[]>> GetAllClaims()
        {
            var claims = await _context.InitiateClaims
                .Include(i => i.TreatmentPlanEntity)
                .Include(i => i.TreatmentPlanEntity.IPTreatmentPackageEntity)
                .Include(i => i.TreatmentPlanEntity.PatientEntity)
                .Include(i => i.InsurerEntity)
                .ToListAsync();

            return _mapper.Map<InitiateCliamResponseDto[]>(claims);
        }

        [HttpPost("InitiateClaim")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseModel))]
        public async Task<ActionResult<InitiateCliamResponseDto>> InitiateClaim(InitiateClaimRequestDto initiateClaimRequestDto)
        {
            var treatmentPlanEntity = await _context.TreatmentPlans
                .Include(t=>t.IPTreatmentPackageEntity)
                .Include(t=>t.PatientEntity)
                .FirstOrDefaultAsync(t=>t.Id== initiateClaimRequestDto.TreatmentPlanEntityId);
            if (treatmentPlanEntity == null)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No TreatmentPlan by id '{initiateClaimRequestDto.TreatmentPlanEntityId}' is found ",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int)ApplicationStatusCodes.TreatmentPlanEntityNotFound
                };
                //_logger.LogTrace()
                return NotFound(error);
            }

            var insurerEntity = await _context.Insurers
                .FirstOrDefaultAsync(i => i.InsurerPackageName == initiateClaimRequestDto.InsurerPackageName);
            if (insurerEntity == null)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No Insurer by name '{initiateClaimRequestDto.InsurerPackageName}' is found ",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int)ApplicationStatusCodes.InsurerEntityNotFound
                };
                //_logger.LogTrace()
                return NotFound(error);
            }

            if (treatmentPlanEntity.IPTreatmentPackageEntity.Cost > insurerEntity.InsuranceAmountLimit)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message =
                        $"Insurance package {insurerEntity.InsurerPackageName} cover upto {insurerEntity.InsuranceAmountLimit} " +
                        $"but treatment plan cost is {treatmentPlanEntity.IPTreatmentPackageEntity.Cost}",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int) ApplicationStatusCodes.ExceedInsuraceClaimAmount
                };
                return BadRequest(error);
            }

            var initiateClaimEntity = new InitiateClaimEntity()
            {
                TreatmentPlanEntityId = treatmentPlanEntity.Id,
                InsurerEntityId = insurerEntity.Id
            };
            await _context.InitiateClaims.AddAsync(initiateClaimEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<InitiateCliamResponseDto>(initiateClaimEntity);
        }
    }
}
