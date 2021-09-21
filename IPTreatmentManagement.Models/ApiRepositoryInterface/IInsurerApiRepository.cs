using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.Dtos.Response;
using Refit;

namespace IPTreatmentManagement.Models.ApiRepositoryInterface
{
    public interface IInsurerApiRepository
    {
        [Get("/api/Insurer")]
        Task<IEnumerable<InsurerResponseDto>> GetAll();

        [Get("/api/Insurer/{InsurerPackageName}")]
        Task<InsurerResponseDto> GetInsurerByPackageName(string insurerPackageName);
    }
}
