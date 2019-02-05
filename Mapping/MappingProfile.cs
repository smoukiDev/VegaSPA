using System.Collections.Generic;
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
                .ForMember(vm => vm.Contact,
                    opt => opt.MapFrom(v => new ContactViewModel
                    {
                        Name = v.ContactInfo.ContactName,
                        Email = v.ContactInfo.ContactEmail,
                        Phone = v.ContactInfo.ContactPhone
                    }))
                .ForMember(vm => vm.Features,
                    opt => opt.MapFrom(v => v.VehicleFeatures
                    .Select(vf => vf.FeatureId)));

            // API Resource to Domain
            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(v => v.Id,
                    opt => opt.Ignore())
                .ForMember(v => v.ContactInfo,
                    opt => opt.MapFrom(vm => new ContactInfo
                    {
                        ContactEmail = vm.Contact.Email,
                        ContactName = vm.Contact.Name,
                        ContactPhone = vm.Contact.Phone
                    }))
                .ForMember(v => v.VehicleFeatures,
                    opt => opt.Ignore())
                .AfterMap((vm, v) => {
                    // Remove unselected features
                    var removedFeatures = new List<VehicleFeature>(); 
                    foreach (var feature in v.VehicleFeatures)
                    {
                        if(!vm.Features.Contains(feature.FeatureId))
                        {
                            removedFeatures.Add(feature);
                        }
                    }
                    foreach(var feature in removedFeatures)
                    {
                        v.VehicleFeatures.Remove(feature);
                    }
                    // Add new features
                    var addedFeatures = new List<VehicleFeature>();
                    foreach (var id in vm.Features)
                    {
                        if(!v.VehicleFeatures.Any(f => f.FeatureId == id))
                        {
                            v.VehicleFeatures.Add(new VehicleFeature{FeatureId = id});
                        }
                    }
                });
        }
    }
}