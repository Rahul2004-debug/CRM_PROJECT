using System.ComponentModel.DataAnnotations;

namespace CRM.API.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } = "SalesRep";

        public bool IsActive { get; set; } = true;

        public int FailedLoginAttempts { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // OTP RESET
        public string? ResetToken { get; set; }

        public DateTime? ResetTokenExpiry { get; set; }
    }
}
