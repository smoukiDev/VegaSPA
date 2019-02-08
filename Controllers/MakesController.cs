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
    public class MakesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MakesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("/api/makes")]
        public async Task<IActionResult> GetMakes()
        {
            // TODO: IEnumerable vs List
            var result = await _unitOfWork.Makes.GetWithModels();
            return Ok(_mapper.Map<List<Make>, List<MakeResource>>(result.ToList()));
        }
    }
}