using System.Collections.Generic;

using PrismMetroSample.Infrastructure.Models;

namespace PrismMetroSample.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        public List<Patient> GetAllPatients()
        {
            List<Patient> allPatient = new List<Patient>()
            {
                new Patient(){Id=1,Name="Lina",Age=18,Sex="F",RoomNo="A-501"},
                new Patient(){Id=2,Name="Ryzen",Age=24,Sex="M",RoomNo="B-610"},
                new Patient(){Id=3,Name="Joy",Age=20,Sex="M",RoomNo="C-620"},
                new Patient(){Id=4,Name="Jack",Age=40,Sex="M",RoomNo="D-520"},
            };
            return allPatient;
        }
    }
}
