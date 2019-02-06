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
            this.Features = new Collection<KeyValuePairResource>();
        }
        public int Id { get; set; }

        public KeyValuePairResource Model { get; set; }

        public KeyValuePairResource Make { get; set; }
               
        public bool IsRegistered { get; set; }
        
        public ContactResource Contact { get; set; }

        public DateTime LastModified { get; set; }

        public ICollection<KeyValuePairResource> Features { get; set; }
    }
}