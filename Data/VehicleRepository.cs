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

        public async Task<IEnumerable<Vehicle>> GetCompleteVehiclesAsync(VehicleQuery queryObject = null)
        {
            var query = base.context.Vehicles
                .Include(v => v.VehicleFeatures)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .AsQueryable();

            queryObject = queryObject ?? new VehicleQuery();
            
            if (queryObject.MakeId.HasValue)
            {
                query = query
                    .Where(v => v.Model.MakeId == queryObject.MakeId.Value);
            }

            if (queryObject.SortBy == "make")
            {
                if (queryObject.IsSortAscending)
                {
                    query = query.OrderBy(v => v.Model.Make.Name);
                }
                else
                {
                    query = query.OrderByDescending(v => v.Model.Make.Name);
                }
            }

            if (queryObject.SortBy == "model")
            {
                if (queryObject.IsSortAscending)
                {
                    query = query.OrderBy(v => v.Model.Name);
                }
                else
                {
                    query = query.OrderByDescending(v => v.Model.Name);
                }
            }

            if (queryObject.SortBy == "contactName")
            {
                if (queryObject.IsSortAscending)
                {
                    query = query.OrderBy(v => v.ContactInfo.ContactName);
                }
                else
                {
                    query = query.OrderByDescending(v => v.ContactInfo.ContactName);
                }
            }

            if (queryObject.SortBy == "id")
            {
                if (queryObject.IsSortAscending)
                {
                    query = query.OrderBy(v => v.Id);
                }
                else
                {
                    query = query.OrderByDescending(v => v.Id);
                }
            }

            return await query.ToListAsync();
        }
    }
}