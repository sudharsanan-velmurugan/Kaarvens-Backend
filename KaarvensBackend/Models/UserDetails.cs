using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KaarvensBackend.Models
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
