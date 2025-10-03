using System.ComponentModel.DataAnnotations;

namespace MedicalReports.Data.Entities
{
    public class GuidelineEntity
    {
        [Key]
        public int Id { get; set; }

        // Cholesterol
        public string CholesterolTotalOptimal { get; set; } = string.Empty;
        public string CholesterolTotalNeedsAttention { get; set; } = string.Empty;
        public string CholesterolTotalSeriousIssue { get; set; } = string.Empty;

        public string CholesterolHdlOptimal { get; set; } = string.Empty;
        public string CholesterolHdlNeedsAttention { get; set; } = string.Empty;
        public string CholesterolHdlSeriousIssue { get; set; } = string.Empty;

        public string CholesterolLdlOptimal { get; set; } = string.Empty;
        public string CholesterolLdlNeedsAttention { get; set; } = string.Empty;
        public string CholesterolLdlSeriousIssue { get; set; } = string.Empty;

        // Blood sugar
        public string BloodSugarOptimal { get; set; } = string.Empty;
        public string BloodSugarNeedsAttention { get; set; } = string.Empty;
        public string BloodSugarSeriousIssue { get; set; } = string.Empty;

        // Blood pressure
        public string BloodPressureOptimalSystolic { get; set; } = string.Empty;
        public string BloodPressureOptimalDiastolic { get; set; } = string.Empty;
        public string BloodPressureNeedsAttentionSystolic { get; set; } = string.Empty;
        public string BloodPressureNeedsAttentionDiastolic { get; set; } = string.Empty;
        public string BloodPressureSeriousIssueSystolic { get; set; } = string.Empty;
        public string BloodPressureSeriousIssueDiastolic { get; set; } = string.Empty;

        // Lifestyle
        public string ExerciseWeeklyMinutesOptimal { get; set; } = string.Empty;
        public string ExerciseWeeklyMinutesNeedsAttention { get; set; } = string.Empty;
        public string ExerciseWeeklyMinutesSeriousIssue { get; set; } = string.Empty;

        public string SleepQualityOptimal { get; set; } = string.Empty;
        public string SleepQualityNeedsAttention { get; set; } = string.Empty;
        public string SleepQualitySeriousIssue { get; set; } = string.Empty;

        public string StressLevelsOptimal { get; set; } = string.Empty;
        public string StressLevelsNeedsAttention { get; set; } = string.Empty;
        public string StressLevelsSeriousIssue { get; set; } = string.Empty;

        public string DietQualityOptimal { get; set; } = string.Empty;
        public string DietQualityNeedsAttention { get; set; } = string.Empty;
        public string DietQualitySeriousIssue { get; set; } = string.Empty;
    }
}
