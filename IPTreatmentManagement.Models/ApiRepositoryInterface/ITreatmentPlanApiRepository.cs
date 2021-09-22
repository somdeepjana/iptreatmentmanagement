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
    public interface ITreatmentPlanApiRepository
    {
        [Get("/api/TreatmentPlans")]
        Task<IEnumerable<TreatmentPlanResponseDTO>> GetAll();

        [Post("/api/TreatmentPlans/FormulateTreatmentTimetable")]
        Task<TreatmentPlanResponseDTO> GenerateTreatmentPlan(PatientRequestDTO patient);
    }
}
