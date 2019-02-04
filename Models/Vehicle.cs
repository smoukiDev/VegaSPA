using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VegaSPA.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public Vehicle()
        {
            this.VehicleFeatures = new Collection<VehicleFeature>();
        }
        public int Id { get; set; }

        public int ModelId { get; set; }

        public Model Model { get; set; }
               
        public bool IsRegistered { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public DateTime LastModified { get; set; }

        public ICollection<VehicleFeature> VehicleFeatures { get; set; }
    }
}