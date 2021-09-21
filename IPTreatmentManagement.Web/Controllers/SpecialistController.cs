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
    public class SpecialistController : Controller
    {
        private readonly ISpecialistApiRepository _specialistRepository;

        public SpecialistController(ISpecialistApiRepository specialistRepository)
        {
            _specialistRepository = specialistRepository;
        }
        public async Task<IActionResult> Index()
        {
            var specialists = await _specialistRepository.GetAll();
            return View(specialists);
        }
    }
}
