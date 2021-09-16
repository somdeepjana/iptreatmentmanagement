using AutoMapper;
using IPTreatmentManagement.Models.Dtos.Response;
using IPTreatmentManagement.Models.RepositorieInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISpecialistRepository _specialistRepository;

        public SpecialistsController(
            IMapper mapper,
            ISpecialistRepository specialistRepository
            )
        {
            _mapper = mapper;
            _specialistRepository = specialistRepository;
        }

        /// <summary>
        /// Get all Specialist Details
        /// </summary>
        /// <returns>list of Specialist</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SpecialistResponseDto[]>> GetAllSpecialists()
        {
            var specialists = await _specialistRepository.GetAllAsync();

            return _mapper.Map<SpecialistResponseDto[]>(specialists);
        }
    }
}
