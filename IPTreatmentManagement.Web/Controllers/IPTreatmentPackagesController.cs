using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using IPTreatmentManagement.Models.ApiRepositoryInterface;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.OperationalModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Refit;

namespace IPTreatmentManagement.Web.Controllers
{
    public class IPTreatmentPackagesController : Controller
    {
        private readonly ILogger<IPTreatmentPackagesController> _logger;
        private readonly IIPTreatmentPackageApiRepository _treatmentPackageApiRepository;

        public IPTreatmentPackagesController(ILogger<IPTreatmentPackagesController> logger,
            IIPTreatmentPackageApiRepository treatmentPackageApiRepository)
        {
            _logger = logger;
            _treatmentPackageApiRepository = treatmentPackageApiRepository;
        }
        public async Task<IActionResult> Index()
        {
            var treatmentPackages = await _treatmentPackageApiRepository.GetAll();
            return View(treatmentPackages);
        }

        public async Task<IActionResult> Details(string treatmentPackageName)
        {
            try
            {
                var treatmentPackage = await _treatmentPackageApiRepository.GetByPackageName(treatmentPackageName);
                return View(treatmentPackage);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorResponseModel>();
                if (error.ApplicationStatusCode == (int) ApplicationStatusCodes.IPTreatmentPackageEntityNotFound)
                    return NotFound(error);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
