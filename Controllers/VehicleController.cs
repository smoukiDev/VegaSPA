using System;
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