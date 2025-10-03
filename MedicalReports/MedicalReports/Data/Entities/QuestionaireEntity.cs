using System.ComponentModel.DataAnnotations;

namespace MedicalReports.Data.Entities
{
    public class QuestionaireEntity
    {
        [Key]
        public int Id { get; set; }

        public int ExerciseWeeklyMinutes { get; set; }

        public string? SleepQuality { get; set; }
        public string? StressLevels { get; set; }
        public string? DietQuality { get; set; }

        // Foreign key
        public int MedicalDataId { get; set; }
        public MedicalDataEntity? MedicalData { get; set; }
    }
}
