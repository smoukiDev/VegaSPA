using System.Threading.Tasks;
using VegaSPA.Models;

namespace VegaSPA.Data
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetCompleteVehicleAsync(int id);

        Task<Vehicle> GetWithVehicleFeaturesAsync(int id);
        
        Task<Vehicle> GetAsync(int id);

        void Add(Vehicle vehicle);

        void Remove(Vehicle vehicle);
    }
}