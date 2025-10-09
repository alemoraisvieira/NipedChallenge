using System.ComponentModel.DataAnnotations;

namespace MedicalReports.Data.Entities
{
    public class MedicalDataEntity
    {
        [Key]
        public int Id { get; set; }

        public BloodworkEntity? Bloodwork { get; set; }
        public QuestionaireEntity? Questionnaire { get; set; }

        // Foreign key
        public string ClientId { get; set; } = string.Empty;
        public ClientEntity? Client { get; set; }
    }
}
