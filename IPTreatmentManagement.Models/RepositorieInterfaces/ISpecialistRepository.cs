using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.RepositorieInterfaces
{
    public interface ISpecialistRepository : IRepository
    {
        Task<IEnumerable<SpecialistEntity>> GetAll();
    }
}
