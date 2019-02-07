using System.Threading.Tasks;
using VegaSPA.Models;

namespace VegaSPA.Data
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        
        void Add(Vehicle vehicle);

        void Remove(Vehicle vehicle);
    }
}