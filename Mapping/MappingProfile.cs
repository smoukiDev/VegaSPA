using System.Linq;
using AutoMapper;
using VegaSPA.Mapping.Models;
using VegaSPA.Models;

namespace VegaSPA.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<Make, MakeViewModel>();
            CreateMap<Model, ModelViewModel>();
            CreateMap<Feature, FeatureViewModel>();
            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(vrm => vrm.Contact,
                    opt => opt.MapFrom(v => new ContactViewModel
                    {
                        Name = v.ContactInfo.ContactName,
                        Email = v.ContactInfo.ContactEmail,
                        Phone = v.ContactInfo.ContactPhone
                    }))
                .ForMember(vrm => vrm.Features,
                    opt => opt.MapFrom(v => v.VehicleFeatures
                    .Select(vf => vf.FeatureId)));

            // API Resource to Domain
            CreateMap<VehicleViewModel, Vehicle>()
                .ForPath(v => v.ContactInfo.ContactName,
                    opt => opt.MapFrom(vrm => vrm.Contact.Name))
                .ForPath(v => v.ContactInfo.ContactEmail,
                    opt => opt.MapFrom(vrm => vrm.Contact.Email))
                .ForPath(v => v.ContactInfo.ContactPhone,
                    opt => opt.MapFrom(vrm => vrm.Contact.Phone))
                .ForMember(v => v.VehicleFeatures,
                    opt => opt.MapFrom(vrm => vrm.Features
                    .Select(id => new VehicleFeature {FeatureId = id})));
        }
    }
}