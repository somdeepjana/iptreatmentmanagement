using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.Dtos.Response;
using Refit;

namespace IPTreatmentManagement.Models.ApiRepositoryInterface
{
    public interface IIPTreatmentPackageApiRepository
    {
        [Get("/api/IPTreatmentPackages")]
        Task<IEnumerable<IPTreatmentPackageResponseDto>> GetAll();

        [Get("/api/IPTreatmentPackages/{packageName}")]
        Task<IPTreatmentPackageResponseDto> GetByPackageName(string packageName);
    }
}
