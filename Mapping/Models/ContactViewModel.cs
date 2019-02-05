using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VegaSPA.Mapping.Models
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }        
    }
}