using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VegaSPA.Data;
using VegaSPA.Mapping.Models;
using VegaSPA.Models;

namespace VegaSPA.Controllers
{
    [Route("/api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly VegaDbContext _context;

        public VehicleController(IMapper mapper, VegaDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleViewModel vehicleModel)
        {
            if(!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var model = await _context.Models.FindAsync(vehicleModel.ModelId);
            if(model == null)
            {
                ModelState.AddModelError(nameof(vehicleModel.ModelId), "Invalid model id." );
                return this.BadRequest(ModelState);
            }
            if(vehicleModel.Features.Count == 0)
            {
                ModelState.AddModelError(nameof(vehicleModel.Features), "Vehicle should contain at least one feature." );
                return this.BadRequest(ModelState);
            }
            foreach (var featureId in vehicleModel.Features)
            {
                try
                {
                    _context.Features.First(f => f.Id == featureId);
                }
                catch(InvalidOperationException ex)
                {
                    ModelState.AddModelError(nameof(vehicleModel.Features), $"Invalid feature has been detected. {ex.Message}" );
                    return this.BadRequest(ModelState);
                }
                
            }
            
            
            var vehicle = _mapper.Map<VehicleViewModel, Vehicle>(vehicleModel);            
            // TODO: Transfer to db
            vehicle.LastModified = DateTime.Now;            
            _context.Add<Vehicle>(vehicle);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<Vehicle, VehicleViewModel>(vehicle);

            return this.Created(Request.Path.Value, result);
        }
    }
}