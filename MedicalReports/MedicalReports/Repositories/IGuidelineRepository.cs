using MedicalReports.Models;

namespace MedicalReports.Repositories
{
    public interface IGuidelineRepository
    {
        Task<MedicalGuidelines> GetMedicalGuidelinesAsync();
        Task SaveMedicalGuidelinesAsync(MedicalGuidelines guidelines);
    }
}
