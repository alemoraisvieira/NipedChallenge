using AutoMapper;
using MedicalReports.Data;
using MedicalReports.Data.Entities;
using MedicalReports.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalReports.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly MedicalDataContext _context;
        private readonly IMapper _mapper;

        public ClientRepository(MedicalDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            var entities = await _context.Clients
                .Include(c => c.MedicalData).ThenInclude(m => m.Bloodwork)
                .Include(c => c.MedicalData).ThenInclude(m => m.Questionnaire)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Client>>(entities);
        }

        public async Task<Client?> GetClientByIdAsync(string id)
        {
            var entity = await _context.Clients
                .Include(c => c.MedicalData).ThenInclude(m => m.Bloodwork)
                .Include(c => c.MedicalData).ThenInclude(m => m.Questionnaire)
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<Client?>(entity);
        }

        public async Task AddClientAsync(Client client)
        {
            var entity = _mapper.Map<ClientEntity>(client);
            _context.Clients.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            var existingEntity = await _context.Clients
                .Include(c => c.MedicalData).ThenInclude(m => m.Bloodwork)
                .Include(c => c.MedicalData).ThenInclude(m => m.Questionnaire)
                .FirstOrDefaultAsync(c => c.Id == client.Id);

            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Client with ID {client.Id} not found.");
            }
            _mapper.Map(client, existingEntity);

            await _context.SaveChangesAsync();
        }
    }
}
