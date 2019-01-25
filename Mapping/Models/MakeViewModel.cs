using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VegaSPA.Mapping.Models
{
    public class MakeViewModel
    {
        public MakeViewModel()
        {
            Models = new Collection<ModelViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelViewModel> Models {get; set;}

    }
}