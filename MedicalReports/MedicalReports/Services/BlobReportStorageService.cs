using Azure.Storage.Blobs;
using MedicalReports.Models;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MedicalReports.Services
{
    public class BlobReportStorageService : IBlobReportStorageService
    {
        private readonly ILogger<BlobReportStorageService> _logger;
        private readonly BlobContainerClient _blobContainerClient;
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public BlobReportStorageService(
            ILogger<BlobReportStorageService> logger,
            BlobContainerClient blobContainerClient)
        {
            _logger = logger;
            _blobContainerClient = blobContainerClient;
        }

        public async Task SaveReportAsync(ClientHealthReport report)
        {
            try
            {
                var blobName = $"reports/{report.ClientId}/{DateTime.UtcNow:yyyyMMddHHmmss}_report.json";
                var blobClient = _blobContainerClient.GetBlobClient(blobName);

                var reportJson = JsonSerializer.Serialize(report, JsonOptions);

                using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(reportJson));
                await blobClient.UploadAsync(stream, overwrite: true);

                _logger.LogInformation($"Report for client {report.ClientId} saved to blob storage: {blobName}");
            }
            catch (Exception ex)
            {
                string errorMessage = $"Failed to save report for client {report.ClientId} to blob storage.";
                _logger.LogError(ex, errorMessage);
                throw;
            }
        }
    }
}
