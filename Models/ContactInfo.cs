using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VegaSPA.Models
{
    [Owned]
    public class ContactInfo
    {
        // TODO: Special Attributes -> EmailAdress and Phone       
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }

        [StringLength(255)]
        public string ContactEmail { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }
    }
}