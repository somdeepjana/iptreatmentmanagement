using AutoMapper;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.RepositorieInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        private readonly IIPTreatmentPackageRepository _iPTreatmentPackageRepository;

        public IPTreatmentPackagesController(
            IMapper mapper,
            IIPTreatmentPackageRepository iPTreatmentPackageRepository
            )
        {
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
                return NotFound();

            return _mapper.Map<IPTreatmentPackageResponseDto>(iPTreatmentPackage);
        }
    }
}
