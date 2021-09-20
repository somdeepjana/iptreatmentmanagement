﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IPTreatmentManagement.Models.ApiRepositoryInterface;
using IPTreatmentManagement.Models.Dtos.Response;

namespace IPTreatmentManagement.Web.Controllers
{
    public class IPTreatmentPackagesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IIPTreatmentPackageApiRepository _treatmentPackageApiRepository;

        public IPTreatmentPackagesController(IMapper mapper,
            IIPTreatmentPackageApiRepository treatmentPackageApiRepository)
        {
            _mapper = mapper;
            _treatmentPackageApiRepository = treatmentPackageApiRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _treatmentPackageApiRepository.GetAll());
        }

        public async Task<IActionResult> Details(string treatmentPackageName)
        {
            return View(await _treatmentPackageApiRepository.GetByPackageName(treatmentPackageName));
        }
    }
}
