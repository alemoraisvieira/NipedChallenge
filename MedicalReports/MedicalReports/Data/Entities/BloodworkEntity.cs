using System.ComponentModel.DataAnnotations;

namespace MedicalReports.Data.Entities
{
    public class BloodworkEntity
    {
        [Key]
        public int Id { get; set; }

        // Cholesterol values
        public int CholesterolTotal { get; set; }
        public int CholesterolHdl { get; set; }
        public int CholesterolLdl { get; set; }

        // Other blood metrics
        public int BloodSugar { get; set; }
        public int BloodPressureSystolic { get; set; }
        public int BloodPressureDiastolic { get; set; }

        // Foreign key
        public int MedicalDataId { get; set; }
        public MedicalDataEntity? MedicalData { get; set; }
    }
}
