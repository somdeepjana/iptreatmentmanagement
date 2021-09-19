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
            var insurers = await _context.Insurers.ToListAsync();
            return _mapper.Map<InsurerResponseDto[]>(insurers);
        }

        [HttpGet]
        [Route("{InsurerPackageName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(ErrorResponseModel))]
        public async Task<ActionResult<InsurerResponseDto>> GetInsurerByPackageName(string InsurerPackageName)
        {
            var insurer = await _context.Insurers.FirstOrDefaultAsync(i => i.InsurerPackageName == InsurerPackageName);
            if(insurer is null)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No Insurer with '{InsurerPackageName}' found",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int)ApplicationStatusCodes.InsurerEntityNotFound
                };
                return NotFound(error);
            }

            return _mapper.Map<InsurerResponseDto>(insurer);
        }

    }
}
