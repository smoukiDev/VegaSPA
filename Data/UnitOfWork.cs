using System.Threading.Tasks;
using VegaSPA.Core;
using VegaSPA.Core.Models;

namespace VegaSPA.Data
{
    // TODO: IDisposable for Repositories and UnitOfWork
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext _context;
        private IMakeRepository _makes;

        private IRepository<Model> _models;
        private IRepository<Feature> _features;
        private IVehicleRepository _vehicles;

        public UnitOfWork(VegaDbContext context)
        {
            _context = context;
        }

        public IMakeRepository Makes
        {
            get
            {
                return _makes = _makes ?? new MakeRepository(_context);
            }
        } 

        public IRepository<Feature> Features
        {
            get
            {
                return _features = _features ?? new VegaRepository<Feature>(_context);
            }
        }
        public IVehicleRepository Vehicles
        {
            get
            {
                return _vehicles = _vehicles ?? new VehicleRepository(_context);
            }
        }

        public IRepository<Model> Models
        {
            get
            {
                return _models = _models ?? new VegaRepository<Model>(_context);
            }
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}