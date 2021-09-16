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
        private readonly IIPTreatmentPackageRepository _iPTreatmentPackageRepository;

        public IPTreatmentPackagesController(IIPTreatmentPackageRepository iPTreatmentPackageRepository)
        {
            _iPTreatmentPackageRepository = iPTreatmentPackageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIPTreatmentPackages()
        {
            return Ok("all Treatment packages");
        }

        [HttpGet("{packageName}")]
        public async Task<IActionResult> GetIPTreatmentPackageByName(string packageName)
        {
            return Ok("a package details");
        }
    }
}
