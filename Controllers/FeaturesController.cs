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
    public class FeaturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FeaturesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("/api/features")]
        public async Task<IActionResult> GetFeatures()
        {
            var result = await _unitOfWork.Features.GetAllAsync();
            return Ok(_mapper.Map<List<Feature>, List<KeyValuePairResource>>(result.ToList()));
        }        
    }
}