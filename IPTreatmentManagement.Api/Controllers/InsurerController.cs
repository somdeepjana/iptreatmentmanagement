using AutoMapper;
using IPTreatmentManagement.EFCore.Data;
using IPTreatmentManagement.Models.Dtos.Response;
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
    public class InsurerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<InsurerController> _logger;

        public InsurerController(
            ApplicationDbContext context, 
            IMapper mapper, 
            ILogger<InsurerController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all Insurer details
        /// </summary>
        /// <returns>list of Insurers</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InsurerResponseDto[]>> GetAllInsurerDetail()
        {
            var insurers = await _context.Insurers.Include(i=>i.IPTreatmentPackageEntity).ToListAsync();
            return _mapper.Map<InsurerResponseDto[]>(insurers);
        }

        [HttpGet]
        [Route("{packageName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(ErrorResponseModel))]
        public async Task<ActionResult<InsurerResponseDto>> GetInsurerByPackageName(string packageName)
        {
            var insurer = await _context.Insurers.Include(i=>i.IPTreatmentPackageEntity)
                                                 .FirstOrDefaultAsync(i => i.IPTreatmentPackageEntity.TreatmentPackageName == packageName);
            if(insurer is null)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No Insurer with '{packageName}' found",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int)ApplicationStatusCodes.InsurerEntityNotFound
                };
                return NotFound(error);
            }

            return _mapper.Map<InsurerResponseDto>(insurer);
        }

    }
}
