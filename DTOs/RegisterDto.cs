using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }
}