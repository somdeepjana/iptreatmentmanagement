﻿using AutoMapper;
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

        /// <summary>
        /// Get all IPTreatmentPackage details
        /// </summary>
        /// <returns>list of IPTreatmentPackage</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IPTreatmentPackageResponseDto[]>> GetAllIPTreatmentPackages()
        {
            var iPTreatmentPackages = await _iPTreatmentPackageRepository.GetAllAsync();

            return _mapper.Map<IPTreatmentPackageResponseDto[]>(iPTreatmentPackages);
        }

        /// <summary>
        /// Get a specific IPTreatmentPackage Details by given name
        /// </summary>
        /// <param name="packageName">Package name to search with</param>
        /// <returns>IPTreatmentPackage details  if found</returns>
        /// <response code="404">No IPTreatmentPackage found with provided name</response>
        [HttpGet("{packageName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseModel))]
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
