using Microsoft.EntityFrameworkCore;
using TestesAPI.Data;
using TestesAPI.Models;

namespace TestesAPI.Repositories
{
    // Class that implements the IVehicleRepository interface
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        private readonly AppDBContext _context;
        public VehicleRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle>> GetClientByVehicleAsync(int vehicleId)
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.Client)
                .Where(v => v.Id == vehicleId)
                .ToListAsync();

            return vehicles;
        }

        public async Task<Vehicle?> GetVehicleByPlateAsync(string plate)
        {
            return await GetAsync(v => v.Plate == plate);
        }
    }
}