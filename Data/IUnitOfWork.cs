using System.Threading.Tasks;
using VegaSPA.Models;

namespace VegaSPA.Data
{
    public interface IUnitOfWork
    {
        IMakeRepository Makes { get; }

        IRepository<Model> Models { get; }

        IRepository<Feature> Features { get; }

        IVehicleRepository Vehicles { get; }
        Task CompleteAsync();
    }
}