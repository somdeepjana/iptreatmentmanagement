using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IPTreatmentManagement.Api.ConfigurationModels;
using IPTreatmentManagement.Api.Models.Entity;
using IPTreatmentManagement.Models.Dtos.Request;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.OperationalModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IPTreatmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtCredentialConfiguration _jwtCredential;

        public AuthController(IOptions<JwtCredentialConfiguration> jwtCredentialOptions,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtCredential = jwtCredentialOptions.Value;
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
                    UserName = user.UserName,
                    Audience = _jwtCredential.Audience,
                    Issuer = _jwtCredential.Issuer,
                    JwtToken = await GenerateJwtTokenAsync(user)
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

        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration["jwt:Key"]);
            var key = Encoding.UTF8.GetBytes(_jwtCredential.Key);

            var claims = (await _signInManager.CreateUserPrincipalAsync(user)).Claims;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //Issuer = _configuration["jwt:Issuer"],
                //Audience = _configuration["jwt:Audience"],
                Issuer = _jwtCredential.Issuer,
                Audience = _jwtCredential.Audience,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
