using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaSPA.Data;
using VegaSPA.Mapping.Models;
using VegaSPA.Core;
using VegaSPA.Core.Models;

namespace VegaSPA.Controllers
{
    [Route("/api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // TODO: Sample for unit-testing
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            // Validation based on data annotation tags.
            if(!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            // Validation of ModelId foreign key.
            var model = await _unitOfWork.Models.FindAsync(vehicleResource.ModelId);
            if(model == null)
            {
                var message = "Invalid model identifier.";
                ModelState.AddModelError(nameof(vehicleResource.ModelId), message);
                return this.BadRequest(ModelState);
            }

            // Validation of Features set
            // At least one feature should be specified in the set.
            if(vehicleResource.Features.Count == 0)
            {
                var message = "Vehicle should contain at least one feature.";
                ModelState.AddModelError(nameof(vehicleResource.Features), message);
                return this.BadRequest(ModelState);
            }

            // Validation of Features set.
            // Set mustn't contain only distinct values of features identifiers.
            if(vehicleResource.Features.Count() != vehicleResource.Features.Distinct().Count())
            {
                var message = "Invalid features set. Feature identifiers should be distinct.";
                ModelState.AddModelError(nameof(vehicleResource.Features), message);
                return this.BadRequest(ModelState);
            }

            // Validation of Features set.
            // Set must contain only existed features identifiers which store in database.
            foreach (var featureId in vehicleResource.Features)
            {
                try
                {
                    var feature = await _unitOfWork.Features.FindAsync(featureId);
                    if(feature == null)
                    {
                        var message = "Invalid feature identifier. Feature doesn't exist.";
                        throw new ArgumentException(message);
                    }
                }
                catch(ArgumentException e)
                {
                    ModelState.AddModelError(nameof(vehicleResource.Features), e.Message);
                    return this.BadRequest(ModelState);
                }              
            }
            
                   
            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);         
            _unitOfWork.Vehicles.Add(vehicle);
            await _unitOfWork.CompleteAsync();

            vehicle = await _unitOfWork.Vehicles.GetCompleteVehicleAsync(vehicle.Id);

            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            
            // First parameter is the route where record was added.
            return this.Created(Request.Path.Value, result);
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            // Validation based on data annotation tags.
            if(!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var vehicle = await _unitOfWork.Vehicles.GetWithVehicleFeaturesAsync(id);
            
            if(vehicle == null)
            {
                return this.NotFound();
            }
            _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);                     
            await _unitOfWork.CompleteAsync();
            
            vehicle = await _unitOfWork.Vehicles.GetCompleteVehicleAsync(vehicle.Id);

            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            
            return this.Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.FindAsync(id);
            if(vehicle == null)
            {
                return this.NotFound();
            }
            _unitOfWork.Vehicles.Remove(vehicle);
            await _unitOfWork.CompleteAsync();
            return this.Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetCompleteVehicleAsync(id);

            if(vehicle == null)
            {
                return this.NotFound();
            }
            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            
            return this.Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            var result = _mapper.Map< IEnumerable<Vehicle>, IEnumerable<VehicleResource> >(vehicles);
            return Ok(result);
        }
    }
}