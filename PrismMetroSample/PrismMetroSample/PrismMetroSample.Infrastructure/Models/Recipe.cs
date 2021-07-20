using System.Collections.Generic;

namespace PrismMetroSample.Infrastructure.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public List<Medicine> Medicines { get; set; }
    }
}
