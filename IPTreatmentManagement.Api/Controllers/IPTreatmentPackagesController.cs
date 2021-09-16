using AutoMapper;
using IPTreatmentManagement.Api.Models;
using IPTreatmentManagement.Models.Dtos.Response;
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
    public class IPTreatmentPackagesController : ControllerBase
    {
        private readonly ILogger<IPTreatmentPackagesController> _logger;
        private readonly IMapper _mapper;
        private readonly IIPTreatmentPackageRepository _iPTreatmentPackageRepository;

        public IPTreatmentPackagesController(
            ILogger<IPTreatmentPackagesController> logger,
            IMapper mapper,
            IIPTreatmentPackageRepository iPTreatmentPackageRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _iPTreatmentPackageRepository = iPTreatmentPackageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IPTreatmentPackageResponseDto[]>> GetAllIPTreatmentPackages()
        {
            var iPTreatmentPackages = await _iPTreatmentPackageRepository.GetAllAsync();

            return _mapper.Map<IPTreatmentPackageResponseDto[]>(iPTreatmentPackages);
        }

        [HttpGet("{packageName}")]
        public async Task<ActionResult<IPTreatmentPackageResponseDto>> GetIPTreatmentPackageByName(string packageName)
        {
            var iPTreatmentPackage = await _iPTreatmentPackageRepository.GetByNameAsync(packageName);

            if (iPTreatmentPackage == null)
            {
                var error = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No IPTreatmentPackage by name '{packageName}' found",
                    Type = ErrorTypes.UserSideError.ToString()
                };
                //_logger.LogTrace()
                return NotFound(error);
            }

            return _mapper.Map<IPTreatmentPackageResponseDto>(iPTreatmentPackage);
        }
    }
}
