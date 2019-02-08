using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VegaSPA.Models;

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
            return await base.context.Vehicles
                .Include(v => v.VehicleFeatures)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Vehicle> GetWithVehicleFeaturesAsync(int id)
        {
            return await base.context.Vehicles
                .Include(v => v.VehicleFeatures)
                .SingleOrDefaultAsync(v => v.Id == id);
        }
    }
}