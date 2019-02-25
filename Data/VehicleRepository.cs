using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VegaSPA.Core;
using VegaSPA.Core.Models;
using VegaSPA.Extensions;

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

            var columnMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = (v) => v.Model.Make.Name,
                ["model"] = (v) => v.Model.Name,
                ["contactName"] = (v) => v.ContactInfo.ContactName,
            };

            query = query.ApplyOrdering(queryObject, columnMap);

            return await query.ToListAsync();
        }
    }
}