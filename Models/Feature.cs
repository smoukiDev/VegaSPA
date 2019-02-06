using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VegaSPA.Models
{
    [Table("Features")]
    public class Feature
    {
        public Feature()
        {
            this.VehicleFeatures = new Collection<VehicleFeature>();         
        }
        
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<VehicleFeature> VehicleFeatures { get; set; }
    }
}