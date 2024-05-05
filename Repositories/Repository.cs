

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TestesAPI.Data;

namespace TestesAPI.Repositories
{
    // Generic class that implements IRepository interface
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDBContext _context;

        public Repository(AppDBContext context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
    }
}