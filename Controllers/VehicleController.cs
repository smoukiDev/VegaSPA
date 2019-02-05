using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // TODO: Sample for unit-testing
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleViewModel vehicleModel)
        {
            // Validation based on data annotation tags.
            if(!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            // Validation of ModelId foreign key.
            var model = await _context.Models.FindAsync(vehicleModel.ModelId);
            if(model == null)
            {
                ModelState.AddModelError(nameof(vehicleModel.ModelId), "Invalid model identifier." );
                return this.BadRequest(ModelState);
            }

            // Validation of Features set
            // At least one feature should be specified in the set.
            if(vehicleModel.Features.Count == 0)
            {
                ModelState.AddModelError(nameof(vehicleModel.Features), "Vehicle should contain at least one feature." );
                return this.BadRequest(ModelState);
            }

            // Validation of Features set.
            // Set mustn't contain only distinct values of features identifiers.
            if(vehicleModel.Features.Count() != vehicleModel.Features.Distinct().Count())
            {
                ModelState.AddModelError(nameof(vehicleModel.Features), "Invalid features set. Feature identifiers should be distinct." );
                return this.BadRequest(ModelState);
            }

            // Validation of Features set.
            // Set must contain only existed features identifiers which store in database.
            foreach (var featureId in vehicleModel.Features)
            {
                try
                {
                    _context.Features.First(f => f.Id == featureId);
                }
                catch(InvalidOperationException)
                {
                    ModelState.AddModelError(nameof(vehicleModel.Features), $"Invalid feature identifier. Feature doesn't exist." );
                    return this.BadRequest(ModelState);
                }              
            }
            
                   
            var vehicle = _mapper.Map<VehicleViewModel, Vehicle>(vehicleModel);         
            _context.Add<Vehicle>(vehicle);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<Vehicle, VehicleViewModel>(vehicle);
            
            // First parameter is the route where record was added.
            return this.Created(Request.Path.Value, result);
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleViewModel vehicleModel)
        {
            // Validation based on data annotation tags.
            if(!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleFeatures)
                .SingleOrDefaultAsync(v => v.Id == id);
            
            if(vehicle == null)
            {
                return this.NotFound();
            }
            _mapper.Map<VehicleViewModel, Vehicle>(vehicleModel, vehicle);                     
            await _context.SaveChangesAsync();
            var result = _mapper.Map<Vehicle, VehicleViewModel>(vehicle);
            
            return this.Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if(vehicle == null)
            {
                return this.NotFound();
            }
            _context.Remove(vehicle);
            await _context.SaveChangesAsync();
            return this.Ok(id);
        }
    }
}