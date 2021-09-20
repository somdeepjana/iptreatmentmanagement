using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IPTreatmentManagement.Api.Seeds;
using IPTreatmentManagement.Models.Dtos.Response;

namespace IPTreatmentManagement.Web.Controllers
{
    public class IPTreatmentPackagesController : Controller
    {
        private readonly IMapper _mapper;

        public IPTreatmentPackagesController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(_mapper.Map<IPTreatmentPackageResponseDto[]>(new IPTreatmentPackageSeedData().GetAll));
        }

        public IActionResult Details(string treatmentPackageName)
        {
            return View();
        }
    }
}
