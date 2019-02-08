using System.Collections.Generic;
using System.Threading.Tasks;
using VegaSPA.Core.Models;

namespace VegaSPA.Core
{
    public interface IMakeRepository : IRepository<Make>
    {
         Task<IEnumerable<Make>> GetWithModels();
    }
}