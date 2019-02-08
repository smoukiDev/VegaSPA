using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VegaSPA.Models;

namespace VegaSPA.Data
{
    public class MakeRepository : VegaRepository<Make>, IMakeRepository
    {
        public MakeRepository(VegaDbContext context)
            :base(context)
        {            
        }

        async Task<IEnumerable<Make>> IMakeRepository.GetWithModels()
        {
            return await base.context.Makes
                .Include(m => m.Models)
                .ToListAsync();        
        }
    }
}