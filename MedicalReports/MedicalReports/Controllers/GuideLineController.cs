using MedicalReports.Models;
using MedicalReports.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalReports.Controllers
{
    [Authorize(Policy = "GuidelineManagerPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class GuidelinesController : ControllerBase
    {
        private readonly IGuidelineRepository _repository;
        private readonly ILogger<GuidelinesController> _logger;

        public GuidelinesController(IGuidelineRepository repository, ILogger<GuidelinesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(MedicalGuidelines), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            var guidelines = await _repository.GetMedicalGuidelinesAsync();
            if (guidelines == null)
            {
                return Problem(
                    title: "No guidelines available",
                    detail: "The system does not have any medical guidelines configured.",
                    statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(guidelines);
        }

        [HttpPut]
        [ProducesResponseType(typeof(MedicalGuidelines), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] MedicalGuidelines guidelines)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            await _repository.SaveMedicalGuidelinesAsync(guidelines);
            _logger.LogInformation("Medical guidelines successfully updated.");
            return Ok(guidelines);
        }
    }
}
