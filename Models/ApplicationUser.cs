using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerceShoppingApplication.Models
{
    public class ApplicationUser : IdentityUser 
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }

    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string? Role { get; set; }

    }
}
