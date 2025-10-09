using MedicalReports.Models;

namespace MedicalReports.Services
{
    public interface IHealthReportComposer
    {
        ClientHealthReport BuildReport(Client client, MedicalGuidelines guidelines);
    }
}