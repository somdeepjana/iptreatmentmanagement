using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPTreatmentManagement.Api.Models.Entity;
using IPTreatmentManagement.Models.Dtos.Request;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.OperationalModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace IPTreatmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<JwtTokenResponseDto>> Authenticate(UserLoginRequestDto userLoginRequestDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginRequestDto.Username);
            if (user == null)
            {
                var userNotFounderror = new ErrorResponseModel()
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    Message = $"No User with username {userLoginRequestDto.Username} is found",
                    Type = ErrorTypes.UserSideError.ToString(),
                    ApplicationStatusCode = (int) ApplicationStatusCodes.ApplicationUserNotFound
                };

                return NotFound(userNotFounderror);
            }

            if (await _userManager.CheckPasswordAsync(user, userLoginRequestDto.Password))
            {
                var jwtTokenResponse = new JwtTokenResponseDto()
                {
                    UserName = user.UserName
                };
                return jwtTokenResponse;
            }

            var mismatchCredentialError = new ErrorResponseModel()
            {
                ErrorId = Guid.NewGuid().ToString(),
                Message = $"User credential mismatch",
                Type = ErrorTypes.UserSideError.ToString(),
                ApplicationStatusCode = (int) ApplicationStatusCodes.UserCredentialMismatch
            };

            return BadRequest(mismatchCredentialError);
        }
    }
}
