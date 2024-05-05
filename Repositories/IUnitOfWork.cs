namespace TestesAPI.Repositories
{
    // Interface that defines the methods that must be implemented by the UnitOfWork class
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }
        IVehicleRepository Vehicles { get; }
        Task CommitAsync();
    }
}