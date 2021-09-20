using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Web.Controllers
{
    public class IPTreatmentPackagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(string treatmentPackageName)
        {
            return View();
        }
    }
}
