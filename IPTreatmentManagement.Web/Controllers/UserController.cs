using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.ApiRepositoryInterface;
using IPTreatmentManagement.Models.Dtos.Request;
using IPTreatmentManagement.Web.ConfigurationModels;
using IPTreatmentManagement.Web.ExceptionFilters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IPTreatmentManagement.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApiRepository _userApiRepository;
        private readonly JwtCredentialConfiguration _jwtCredential;

        public UserController(IOptions<JwtCredentialConfiguration> jwtCredentialOptions,
            IUserApiRepository userApiRepository)
        {
            _userApiRepository = userApiRepository;
            _jwtCredential = jwtCredentialOptions.Value;
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
            if (string.IsNullOrEmpty(jwtTokenResponse.JwtToken))
                throw new Exception("token not found int the received response");

            var decoupledToken = validateToken(jwtTokenResponse.JwtToken);

            return RedirectToAction("index", "Home");
        }

        private SecurityToken validateToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = _jwtCredential.Issuer,
                ValidAudience = _jwtCredential.Audience,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtCredential.Key))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken decoupledToken);

            return decoupledToken;
        }
    }
}
