using AutoMapper;
using IPTreatmentManagement.Models.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IPTreatmentManagement.Api.Models.Entity;
using IPTreatmentManagement.EFCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace IPTreatmentManagement.Api.Controllers
{
    //[Authorize(Roles = nameof(UserRoles.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public SpecialistsController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Get all Specialist Details
        /// </summary>
        /// <returns>list of Specialist</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SpecialistResponseDto[]>> GetAllSpecialists()
        {
            var specialists = await _context.Specialists.ToListAsync();

            return _mapper.Map<SpecialistResponseDto[]>(specialists);
        }
    }
}
