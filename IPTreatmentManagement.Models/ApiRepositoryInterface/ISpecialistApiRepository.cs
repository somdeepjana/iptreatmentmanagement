using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.Dtos.Response;
using Refit;

namespace IPTreatmentManagement.Models.ApiRepositoryInterface
{
    public interface ISpecialistApiRepository
    {
        [Get("/api/Specialists")]
        Task<IEnumerable<SpecialistResponseDto>> GetAll();
    }
}
