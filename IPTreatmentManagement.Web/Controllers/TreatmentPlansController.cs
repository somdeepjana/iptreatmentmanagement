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
    public class TreatmentPlansController : Controller
    {
        private readonly ITreatmentPlanApiRepository _treatmentPlanRepository;

        public TreatmentPlansController(ITreatmentPlanApiRepository treatmentPlanRepository)
        {
            _treatmentPlanRepository = treatmentPlanRepository;
        }

        public async Task<IActionResult> Index()
        {
            var treatmentPlans = await _treatmentPlanRepository.GetAll();
            return View(treatmentPlans);
        }

        public async Task<IActionResult> Create(string treatmentPackageName="")
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientRequestDTO patient)
        {
            var newTreatmentPlan = await _treatmentPlanRepository.GenerateTreatmentPlan(patient);

            TempData["Message"] = "Treatment Plan Created";

            return RedirectToAction(nameof(Index));
        }
    }
}
