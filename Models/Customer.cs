using System.ComponentModel.DataAnnotations;

namespace CRM.API.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Company { get; set; }

        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}