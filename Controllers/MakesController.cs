using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaSPA.Data;
using VegaSPA.Mapping.Models;
using VegaSPA.Models;

namespace VegaSPA.Controllers
{
    public class MakesController : ControllerBase
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;

        public MakesController(VegaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("/api/makes")]
        public async Task<IActionResult> GetMakes()
        {
            var result = await _context.Makes.Include(m => m.Models).ToListAsync();
            return Ok(_mapper.Map<List<Make>, List<MakeViewModel>>(result));
        }
    }
}