using System.Threading.Tasks;
using VegaSPA.Models;

namespace VegaSPA.Data
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<Vehicle> GetCompleteVehicleAsync(int id);

        Task<Vehicle> GetWithVehicleFeaturesAsync(int id);
    }
}