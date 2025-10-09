using MedicalReports.Models;

namespace MedicalReports.Services
{
    public interface IBlobReportStorageService
    {
        Task SaveReportAsync(ClientHealthReport report);
    }
}