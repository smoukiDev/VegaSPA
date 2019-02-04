using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VegaSPA.Mapping.Models
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Phone { get; set; }        
    }
}