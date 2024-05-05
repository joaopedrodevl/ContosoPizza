using TestesAPI.Models;

namespace TestesAPI.Repositories
{
    // Interface that defines the methods that must be implemented by the ClientRepository class
    public interface IClientRepository : IRepository<Client>
    {
        Task<IEnumerable<Client>> GetVehiclesByClientAsync(int clientId);
        Task<Client?> GetClientByEmailAsync(string email);
    }
}