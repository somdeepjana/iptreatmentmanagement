using IPTreatmentManagement.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.EFCore.Data.Seeds
{
    public static class SpecialistSeedExtension
    {
        public static bool SeedSpecialists(this ApplicationDbContext context, bool forceCreate = false)
        {
            if (context.Specialists.Any() && !forceCreate)
                return false;

            foreach (var specialist in new SpecialistSeedData().GetAll)
            {
                if (context.Specialists.FirstOrDefault(
                    s => s.Name == specialist.Name) == null)
                    context.Specialists.Add(specialist);
            }

            return context.SaveChanges() > 0;
        }
    }

    public class SpecialistSeedData
    {
        public IEnumerable<SpecialistEntity> GetAll
        {
            get
            {
                return _specialists;
            }
        }

        private IEnumerable<SpecialistEntity> _specialists = new List<SpecialistEntity>
            {
                new SpecialistEntity
                {
                    Name= "Specialist 1",
                    AreaOfExpertise= AilmentDomain.Orthopaedics,
                    ExperienceInYears= 10,
                    ContactNumber= "8546578965"
                },
                new SpecialistEntity
                {
                    Name= "Specialist 2",
                    AreaOfExpertise= AilmentDomain.Orthopaedics,
                    ExperienceInYears= 15,
                    ContactNumber= "5478986525"
                },
                new SpecialistEntity
                {
                    Name= "Specialist 3",
                    AreaOfExpertise= AilmentDomain.Urology,
                    ExperienceInYears= 7,
                    ContactNumber= "7845659825"
                },
                new SpecialistEntity
                {
                    Name= "Specialist 4",
                    AreaOfExpertise= AilmentDomain.Urology,
                    ExperienceInYears= 16,
                    ContactNumber= "9756489852"
                },
                new SpecialistEntity
                {
                    Name= "Specialist 5",
                    AreaOfExpertise= AilmentDomain.Orthopaedics,
                    ExperienceInYears= 5,
                    ContactNumber= "2545879865"
                },
                new SpecialistEntity
                {
                    Name= "Specialist 6",
                    AreaOfExpertise= AilmentDomain.Urology,
                    ExperienceInYears= 4,
                    ContactNumber= "7898654524"
                }
            };
    }
}
