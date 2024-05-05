using Microsoft.EntityFrameworkCore;
using TestesAPI.Data;
using TestesAPI.Models;

namespace TestesAPI.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly AppDBContext _context;
        public ClientRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetVehiclesByClientAsync(int clientId)
        {
            var clients = await _context.Clients
                .Include(c => c.Vehicles)
                .Where(c => c.Id == clientId)
                .ToListAsync();
            
            return clients;
        }

        public async Task<Client?> GetClientByEmailAsync(string email)
        {
            var client = await GetAsync(c => c.Email == email);

            return client;
        }
    }
}