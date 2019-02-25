using System.Collections.Generic;
using System.Threading.Tasks;
using VegaSPA.Core.Models;

namespace VegaSPA.Core
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<Vehicle> GetCompleteVehicleAsync(int id);

        Task<Vehicle> GetWithVehicleFeaturesAsync(int id);

        Task<IEnumerable<Vehicle>> GetCompleteVehiclesAsync(VehicleQuery filter = null);
    }
}