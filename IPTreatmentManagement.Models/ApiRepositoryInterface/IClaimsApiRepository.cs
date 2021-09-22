using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.Dtos.Request;
using IPTreatmentManagement.Models.Dtos.Response;
using Refit;

namespace IPTreatmentManagement.Models.ApiRepositoryInterface
{
    public interface IClaimsApiRepository
    {
        [Get("/api/Claims")]
        Task<IEnumerable<InitiateCliamResponseDto>> GetAll();

        [Post("/api/Claims/InitiateClaim")]
        Task<InitiateCliamResponseDto> InitiateClaim(InitiateClaimRequestDto claimRequest);
    }
}
