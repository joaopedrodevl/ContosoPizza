using System.Linq.Expressions;

namespace TestesAPI.Repositories
{
    // Generic interface that defines the methods that must be implemented by the Repository class
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}