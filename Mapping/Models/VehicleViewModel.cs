using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VegaSPA.Mapping.Models
{
    public class VehicleViewModel
    {
        public VehicleViewModel()
        {
            this.Features = new Collection<int>();
        }
        public int Id { get; set; }

        public int ModelId { get; set; }
               
        public bool IsRegistered { get; set; }

        [Required]
        public ContactViewModel Contact { get; set; }

        public ICollection<int> Features { get; set; }
    }
}