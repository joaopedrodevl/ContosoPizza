using TestesAPI.Data;

namespace TestesAPI.Repositories
{
    // Class that implements the IUnitOfWork interface
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        private IClientRepository? _clients;
        private IVehicleRepository? _vehicles;

        public UnitOfWork(AppDBContext context)
        {
            _context = context;
        }

        // Properties that return the repositories, if they are null, they will be instantiated
        public IClientRepository Clients => _clients ??= new ClientRepository(_context);
        public IVehicleRepository Vehicles => _vehicles ??= new VehicleRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Dispose method that will be called when the object is no longer needed
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}