using System.Collections.Generic;
using System.Threading.Tasks;

namespace VegaSPA.Core
{
    public interface IRepository<T> where T: class
    {
        void Add(T item);

        void Remove(T item);

        Task<T> FindAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}