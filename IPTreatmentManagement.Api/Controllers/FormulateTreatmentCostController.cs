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
    public class FormulateTreatmentCostController : ControllerBase
    {
        private readonly ILogger<FormulateTreatmentCostController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public FormulateTreatmentCostController(ILogger<FormulateTreatmentCostController> logger,
            IMapper mapper,
            ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseModel))]
        public async Task<ActionResult<InitiateCliamResponseDto>> InitiateClaim(InitiateClaimRequestDto initiateClaim)
        {
            var initiateClaimEntity = _mapper.Map<InitiateClaimEntity>(initiateClaim);

            var package = await _context.IPTreatmentPackages
                .FirstOrDefaultAsync(p => p.TreatmentPackageName == initiateClaim.TreatmentPackageName);
            if(package==null)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No IPTreatmentPackage by name '{initiateClaim.TreatmentPackageName}' is found ",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int)ApplicationStatusCodes.IPTreatmentPackageEntityNotFound
                };
                //_logger.LogTrace()
                return NotFound(error);
            }

            initiateClaimEntity.IPTreatmentPackageEntityId = package.Id;
            await _context.AddAsync(initiateClaimEntity);
            await _context.SaveChangesAsync();

            return new InitiateCliamResponseDto() { AmountToBePaid= package.Cost };
        }
    }
}
