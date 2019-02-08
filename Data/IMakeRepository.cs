using System.Collections.Generic;
using System.Threading.Tasks;
using VegaSPA.Models;

namespace VegaSPA.Data
{
    public interface IMakeRepository : IRepository<Make>
    {
         Task<IEnumerable<Make>> GetWithModels();
    }
}