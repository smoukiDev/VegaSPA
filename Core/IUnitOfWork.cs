using System.Threading.Tasks;
using VegaSPA.Core.Models;

namespace VegaSPA.Core
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