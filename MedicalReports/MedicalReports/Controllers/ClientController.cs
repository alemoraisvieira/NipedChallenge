using MedicalReports.Models;
using MedicalReports.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalReports.Controllers
{
    [Authorize(Policy = "ClientEditorPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientRepository clientRepository, ILogger<ClientsController> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        /// <summary>
        /// List all registered clients.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllClientsAsync();
            return Ok(clients);
        }

        /// <summary>
        /// Retrieve a client by identifier.
        /// </summary>
        [HttpGet("{id}", Name = "GetClientById")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            if (client == null)
            {
                _logger.LogInformation("Request for non-existent client {ClientId}", id);
                return Problem(
                    title: "Client not found",
                    detail: $"No client with ID {id} exists in the system.",
                    statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(client);
        }

        /// <summary>
        /// Register a new client.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Client), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] Client client)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            client.Id ??= Guid.NewGuid().ToString();

            if (await _clientRepository.GetClientByIdAsync(client.Id) != null)
            {
                return Problem(
                    title: "Conflict",
                    detail: $"Client with ID {client.Id} already exists.",
                    statusCode: StatusCodes.Status409Conflict);
            }

            await _clientRepository.AddClientAsync(client);
            _logger.LogInformation("Client {ClientId} registered.", client.Id);

            return CreatedAtRoute("GetClientById", new { id = client.Id }, client);
        }

        /// <summary>
        /// Update an existing client.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] Client client)
        {
            if (id != client.Id)
            {
                return Problem(
                    title: "Invalid request",
                    detail: "The provided ID does not match the client's identifier.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var existing = await _clientRepository.GetClientByIdAsync(id);
            if (existing == null)
            {
                return Problem(
                    title: "Not Found",
                    detail: $"Client {id} cannot be updated because it does not exist.",
                    statusCode: StatusCodes.Status404NotFound);
            }

            await _clientRepository.UpdateClientAsync(client);
            _logger.LogInformation("Client {ClientId} updated.", id);

            return NoContent();
        }
    }
}
