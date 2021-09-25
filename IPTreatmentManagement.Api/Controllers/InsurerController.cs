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
using IPTreatmentManagement.Api.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace IPTreatmentManagement.Api.Controllers
{
    [Authorize(Roles = nameof(UserRoles.Admin))]
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

        /// <summary>
        /// Get a specific Insurer Details by given InsurerPackageName
        /// </summary>
        /// <param name="insurerPackageName">Package name to search with</param>
        /// <returns>Insurer details  if found</returns>
        /// <response code="200">Insurer found with provided InsurerPackageName</response>
        /// <response code="404">No Insurer found with provided InsurerPackageName</response>
        [HttpGet]
        [Route("{insurerPackageName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(ErrorResponseModel))]
        public async Task<ActionResult<InsurerResponseDto>> GetInsurerByPackageName(string insurerPackageName)
        {
            var insurer = await _context.Insurers.FirstOrDefaultAsync(i => i.InsurerPackageName == insurerPackageName);
            if(insurer is null)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No Insurer with '{insurerPackageName}' found",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int)ApplicationStatusCodes.InsurerEntityNotFound,
                    ErrorDetails = new Dictionary<string, string>
                    {
                        {"insurerPackageName", "check the insurerPackageName"}
                    }
                };
                return NotFound(error);
            }

            return _mapper.Map<InsurerResponseDto>(insurer);
        }

    }
}
