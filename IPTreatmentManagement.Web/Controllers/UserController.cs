using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.ApiRepositoryInterface;
using IPTreatmentManagement.Models.Dtos.Request;
using IPTreatmentManagement.Models.OperationalModels;
using IPTreatmentManagement.Web.ConfigurationModels;
using IPTreatmentManagement.Web.ExceptionFilters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Refit;

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

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/Home/Index")
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [TypeFilter(typeof(ApiCallExceptionFilter))]
        public async Task<IActionResult> Login(UserLoginRequestDto userLoginRequest, string returnUrl="/Home/Index")
        {
            try
            {
                var jwtTokenResponse = await _userApiRepository.Authenticate(userLoginRequest);
                if (string.IsNullOrEmpty(jwtTokenResponse.JwtToken))
                    throw new Exception("token not found int the received response");

                var decoupledToken = validateToken(jwtTokenResponse.JwtToken);
                var identity = new ClaimsIdentity(decoupledToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim("jwtToken", jwtTokenResponse.JwtToken));
                var principle = new ClaimsPrincipal(identity);

                await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, new AuthenticationProperties());
                //HttpContext.Session.SetString("jwtToken", jwtTokenResponse.JwtToken);
                //HttpContext.Items["jwtToken"] = jwtTokenResponse.JwtToken;

                return LocalRedirect(returnUrl);
            }
            catch (ApiException e)
            {
                var errorContent = e.GetContentAsAsync<ErrorResponseModel>().Result;
                if (errorContent != null)
                {
                    if (errorContent.ApplicationStatusCode == (int) ApplicationStatusCodes.ApplicationUserNotFound)
                    {
                        ModelState.AddModelError("Username", "Username not present");
                        return View(userLoginRequest);
                    }
                    else if(errorContent.ApplicationStatusCode == (int) ApplicationStatusCodes.UserCredentialMismatch)
                    {
                        ModelState.AddModelError("", "Invalid Credential");
                        return View(userLoginRequest);
                    }
                }

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();
            //var test = HttpContext.User.FindFirst("jwtToken");
            //var test = HttpContext.Items["jwtToken"];
            //HttpContext.Items["jwtToken"] = string.Empty;
            await HttpContext.SignOutAsync();

            return RedirectToAction("index", "Home");
        }

        private JwtSecurityToken validateToken(string token)
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

            return tokenHandler.ReadJwtToken(token);
        }
    }
}
