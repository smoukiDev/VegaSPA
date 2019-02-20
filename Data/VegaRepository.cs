using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VegaSPA.Core;

namespace VegaSPA.Data
{
    public class VegaRepository<T>
        : IRepository<T> where T : class
    {
        protected readonly VegaDbContext context;
        private readonly DbSet<T> _dbSet;
        public VegaRepository(VegaDbContext context)
        {
            this.context = context;
            _dbSet = this.context.Set<T>();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }
    }
}