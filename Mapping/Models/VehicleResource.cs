using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VegaSPA.Models;

namespace VegaSPA.Mapping.Models
{
    public class VehicleResource
    {
        public VehicleResource()
        {
            this.Features = new Collection<FeatureResource>();
        }
        public int Id { get; set; }

        public ModelResource Model { get; set; }

        public MakeResource Make { get; set; }
               
        public bool IsRegistered { get; set; }
        
        public ContactResource Contact { get; set; }

        public DateTime LastModified { get; set; }

        public ICollection<FeatureResource> Features { get; set; }
    }
}