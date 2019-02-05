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
                    this.RemoveFeatures(vm, v);
                    this.AddFeatures(vm, v);
                });
        }

        /// <summary>
        /// Add new features
        /// </summary>
        /// <param name="vihicleModel">API Model.</param>
        /// <param name="vehicle">Domain model.</param>
        private void AddFeatures(VehicleViewModel vihicleModel, Vehicle vehicle)
        {
            var addedFeatures = vihicleModel.Features
                .Where(f => !vehicle.VehicleFeatures.Any(vf => f == vf.FeatureId))
                .Select(id => new VehicleFeature{FeatureId = id});
            foreach (var feature in addedFeatures)
            {
                vehicle.VehicleFeatures.Add(feature);
            }
        }

        /// <summary>
        /// Remove unselected features
        /// </summary>
        /// <param name="vehicleModel">API Model.</param>
        /// <param name="vehicle">Domain model.</param>
        private void RemoveFeatures(VehicleViewModel vehicleModel, Vehicle vehicle)
        {
            // TODO: Refactoring using LINQ
            var removedFeatures = new List<VehicleFeature>(); 
            foreach (var feature in vehicle.VehicleFeatures)
            {
                if(!vehicleModel.Features.Contains(feature.FeatureId))
                {
                    removedFeatures.Add(feature);
                }
            }
            foreach(var feature in removedFeatures)
            {
                vehicle.VehicleFeatures.Remove(feature);
            }
        }
    }
}