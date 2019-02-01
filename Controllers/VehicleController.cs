using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VegaSPA.Models;

namespace VegaSPA.Controllers
{
    [Route("/api/vehicles")]
    public class VehicleController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateVehicle([FromBody] Vehicle vehicle)
        {
            return Ok(vehicle);
        }
    }
}