using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VegaSPA.Core;
using VegaSPA.Core.Models;

namespace VegaSPA.Data
{
    // TODO: Overkill get methods and their description
    public class VehicleRepository : VegaRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(VegaDbContext context)
            :base(context)
        {
        }

        public async Task<Vehicle> GetCompleteVehicleAsync(int id)
        {
            var vehicles = await this.GetCompleteVehiclesAsync();
            return vehicles.FirstOrDefault(v => v.Id == id);
        }

        public async Task<Vehicle> GetWithVehicleFeaturesAsync(int id)
        {
            return await base.context.Vehicles
                .Include(v => v.VehicleFeatures)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> GetCompleteVehiclesAsync(VehicleQuery filter = null)
        {
            var query = base.context.Vehicles
                .Include(v => v.VehicleFeatures)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .AsQueryable();

            filter = filter ?? new VehicleQuery();
            
            if (filter.MakeId.HasValue)
            {
                query = query
                    .Where(v => v.Model.MakeId == filter.MakeId.Value);
            }

            return await query.ToListAsync();
        }
    }
}