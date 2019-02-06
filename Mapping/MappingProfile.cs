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
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            // TODO: SaveVehicleResource -> Vehicle Resource
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v => new ContactResource
                    {
                        Name = v.ContactInfo.ContactName,
                        Email = v.ContactInfo.ContactEmail,
                        Phone = v.ContactInfo.ContactPhone
                    }))
                .ForMember(vr => vr.Features,
                    opt => opt.MapFrom(v => v.VehicleFeatures
                    .Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v => new ContactResource
                    {
                        Name = v.ContactInfo.ContactName,
                        Email = v.ContactInfo.ContactEmail,
                        Phone = v.ContactInfo.ContactPhone
                    }))
                .ForMember(vr => vr.Features,
                    opt => opt.MapFrom(v => v.VehicleFeatures
                    .Select(vf => new KeyValuePairResource
                    {
                        Id = vf.Feature.Id,
                        Name = vf.Feature.Name        
                    })))
                .ForMember(vr => vr.Model,
                    opt => opt.MapFrom(v => new KeyValuePairResource
                    {
                        Id = v.Model.Id,
                        Name = v.Model.Name
                    }))
                .ForMember(vr => vr.Make,
                opt => opt.MapFrom(v => v.Model.Make));

            // API Resource to Domain
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id,
                    opt => opt.Ignore())
                .ForMember(v => v.ContactInfo,
                    opt => opt.MapFrom(vr => new ContactInfo
                    {
                        ContactEmail = vr.Contact.Email,
                        ContactName = vr.Contact.Name,
                        ContactPhone = vr.Contact.Phone
                    }))
                .ForMember(v => v.VehicleFeatures,
                    opt => opt.Ignore())
                .AfterMap((vr, v) => {
                    this.RemoveFeatures(vr, v);
                    this.AddFeatures(vr, v);
                });
        }

        /// <summary>
        /// Add new features
        /// </summary>
        /// <param name="vihicleResource">API Model.</param>
        /// <param name="vehicle">Domain model.</param>
        private void AddFeatures(SaveVehicleResource vihicleResource, Vehicle vehicle)
        {
            var addedFeatures = vihicleResource.Features
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
        /// <param name="vehicleResource">API Model.</param>
        /// <param name="vehicle">Domain model.</param>
        private void RemoveFeatures(SaveVehicleResource vehicleResource, Vehicle vehicle)
        {
            var removedFeatures = vehicle.VehicleFeatures.Where(vf => !vehicleResource.Features.Contains(vf.FeatureId));
            // ToList() make independant copy, which is used only by foreash loop.
            foreach (var item in removedFeatures.ToList())
            {
                vehicle.VehicleFeatures.Remove(item);
            }
        }
    }
}