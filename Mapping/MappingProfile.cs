using AutoMapper;
using VegaSPA.Mapping.Models;
using VegaSPA.Models;

namespace VegaSPA.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeViewModel>();
            CreateMap<Model, ModelViewModel>();
        }
    }
}