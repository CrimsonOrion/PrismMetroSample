using System.Collections.Generic;

using PrismMetroSample.Infrastructure.Interceptor.HandlerAttributes;
using PrismMetroSample.Infrastructure.Models;

namespace PrismMetroSample.Infrastructure.Services
{
    [LogHandler]
    public interface IMedicineService
    {
        List<Medicine> GetAllMedicines();

        List<Recipe> GetRecipesByPatientId(int patientId);
    }
}
