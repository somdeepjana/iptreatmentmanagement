using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.ApiRepositoryInterface;
using IPTreatmentManagement.Models.Dtos.Request;
using IPTreatmentManagement.Web.ExceptionFilters;

namespace IPTreatmentManagement.Web.Controllers
{
    [TypeFilter(typeof(ApiCallExceptionFilter))]
    public class ClaimsController : Controller
    {
        private readonly IClaimsApiRepository _claimsRepository;

        public ClaimsController(IClaimsApiRepository claimsRepository)
        {
            _claimsRepository = claimsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var claims = await _claimsRepository.GetAll();
            return View(claims);
        }

        public async Task<IActionResult> InitiateClaim()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InitiateClaim(InitiateClaimRequestDto claimRequest)
        {
            var claims = await _claimsRepository.InitiateClaim(claimRequest);

            TempData["Message"] = "Claim Initiation Request received";

            return RedirectToAction(nameof(Index));
        }
    }
}
