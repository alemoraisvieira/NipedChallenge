using MedicalReports.Domain;
using MedicalReports.Models;
using MedicalReports.Repositories;
using MedicalReports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalReports.Controllers
{
    [Authorize(Policy = "ReportViewerPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IClientRepository _clients;
        private readonly IGuidelineRepository _guidelines;
        private readonly IHealthReportComposer _composer;
        private readonly IBlobReportStorageService _storage;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(
            IClientRepository clients,
            IGuidelineRepository guidelines,
            IHealthReportComposer composer,
            IBlobReportStorageService storage,
            ILogger<ReportsController> logger)
        {
            _clients = clients;
            _guidelines = guidelines;
            _composer = composer;
            _storage = storage;
            _logger = logger;
        }

        /// <summary>
        /// Generate a health report for a specific client.
        /// </summary>
        [HttpGet("{clientId}")]
        [ProducesResponseType(typeof(ClientHealthReport), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerateReportAsync(string clientId)
        {
            var client = await _clients.GetClientByIdAsync(clientId);
            if (client == null)
            {
                return Problem(
                    title: "Client not found",
                    detail: $"Unable to generate report: client {clientId} does not exist.",
                    statusCode: StatusCodes.Status404NotFound);
            }

            var guidelineSet = await _guidelines.GetMedicalGuidelinesAsync();
            if (guidelineSet == null)
            {
                return Problem(
                    title: "Guidelines unavailable",
                    detail: "Medical guidelines could not be retrieved at this time.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            var report = _composer.BuildReport(client, guidelineSet);

            await _storage.SaveReportAsync(report);
            _logger.LogInformation("Generated report for client {ClientId}", clientId);

            return Ok(report);
        }
    }
}