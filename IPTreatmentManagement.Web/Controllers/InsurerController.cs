using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.ApiRepositoryInterface;
using IPTreatmentManagement.Web.ExceptionFilters;

namespace IPTreatmentManagement.Web.Controllers
{
    [TypeFilter(typeof(ApiCallExceptionFilter))]
    public class InsurerController : Controller
    {
        private readonly IInsurerApiRepository _insurerRepository;

        public InsurerController(IInsurerApiRepository insurerRepository)
        {
            _insurerRepository = insurerRepository;
        }
        public async Task<IActionResult> Index()
        {
            var insurers = await _insurerRepository.GetAll();
            return View(insurers);
        }

        public async Task<IActionResult> Details(string insurerPackageName)
        {
            var insurer = await _insurerRepository.GetInsurerByPackageName(insurerPackageName);
            return View(insurer);
        }
    }
}
