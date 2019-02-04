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
                .ForMember(vvm => vvm.Contact,
                    opt => opt.MapFrom(v => new ContactViewModel
                    {
                        Name = v.ContactInfo.ContactName,
                        Email = v.ContactInfo.ContactEmail,
                        Phone = v.ContactInfo.ContactPhone
                    }))
                .ForMember(vvm => vvm.Features,
                    opt => opt.MapFrom(v => v.VehicleFeatures
                    .Select(vf => vf.FeatureId)));

            // API Resource to Domain
            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(v => v.ContactInfo,
                    opt => opt.MapFrom(vvm => new ContactInfo
                    {
                        ContactEmail = vvm.Contact.Email,
                        ContactName = vvm.Contact.Name,
                        ContactPhone = vvm.Contact.Phone
                    }))
                .ForMember(v => v.VehicleFeatures,
                    opt => opt.MapFrom(vvm => vvm.Features
                    .Select(id => new VehicleFeature {FeatureId = id})));
        }
    }
}