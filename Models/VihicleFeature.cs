using System.ComponentModel.DataAnnotations.Schema;

namespace VegaSPA.Models
{
    [Table("VihicleFeatures")]
    public class VihicleFeature
    {
        public int VihicleId { get; set; }
        
        public Vihicle Vihicle { get; set; }

        public int FeatureId { get; set; }

        public Feature Feature { get; set; }
    }
}