using System.Collections.Generic;
using System.Linq;

using PrismMetroSample.Infrastructure.Models;

namespace PrismMetroSample.Infrastructure.Services
{
    public class MedicineService : IMedicineService
    {
        public List<Medicine> GetAllMedicines()
        {
            List<Medicine> allMedicines = new List<Medicine>()
            {
                new Medicine(){Id=1,Name="Angelica",Type="Herbal",Unit="gram"},
                new Medicine(){Id=2,Name="Ginseng",Type="Herbal",Unit="gram"},
                new Medicine(){Id=3,Name="Wolfberry",Type="Herbal",Unit="gram"},
                new Medicine(){Id=4,Name="Penicillin",Type="Western",Unit="bottle"},
                new Medicine(){Id=5,Name="Joan Yulu",Type="Fairy",Unit="drip"}
            };
            return allMedicines;
        }

        public List<Recipe> GetRecipesByPatientId(int patientId)
        {
            List<Medicine> allMedicines = GetAllMedicines();
            List<Recipe> allRecipe = new List<Recipe>()
            {
                new Recipe() {Id=1,Medicines=allMedicines.Where(t=>t.Id>2).ToList(),PatientId=2},
                new Recipe() {Id=1,Medicines=allMedicines.Where(t=>t.Id==2||t.Id==3).ToList(),PatientId=1},
                new Recipe() {Id=1,Medicines=allMedicines.Where(t=>t.Id<4).ToList(),PatientId=3},
                new Recipe() {Id=1,Medicines=allMedicines.Where(t=>t.Id==5).ToList(),PatientId=4},
            };
            return allRecipe.Where(t => t.PatientId == patientId).ToList();
        }

    }
}
