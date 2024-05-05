using TestesAPI.Models;

namespace TestesAPI.Repositories
{
    // Interface that defines the methods that must be implemented by the ClientRepository class
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetClientByVehicleAsync(int vehicleId);
        Task<Vehicle?> GetVehicleByPlateAsync(string plate);
    }
}