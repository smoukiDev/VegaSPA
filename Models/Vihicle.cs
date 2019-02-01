using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VegaSPA.Models
{
    [Table("Vehicles")]
    public class Vihicle
    {
        public Vihicle()
        {
            this.VihicleFeatures = new Collection<VihicleFeature>();
        }
        public int Id { get; set; }

        public int ModelId { get; set; }

        public Model Model { get; set; }
        
        // TODO: Generated NOT NULL in DB?       
        public bool IsRegistered { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public DateTime LastModified { get; set; }

        public ICollection<VihicleFeature> VihicleFeatures { get; set; }
    }
}