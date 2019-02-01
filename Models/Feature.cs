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
            this.VihicleFeatures = new Collection<VihicleFeature>();         
        }
        
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<VihicleFeature> VihicleFeatures { get; set; }
    }
}