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
    public class UserController : Controller
    {
        private readonly IUserApiRepository _userApiRepository;

        public UserController(IUserApiRepository userApiRepository)
        {
            _userApiRepository = userApiRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [TypeFilter(typeof(ApiCallExceptionFilter))]
        public async Task<IActionResult> Login(UserLoginRequestDto userLoginRequest)
        {
            var jwtTokenResponse = await _userApiRepository.Authenticate(userLoginRequest);

            return RedirectToAction("index", "Home");
        }
    }
}
