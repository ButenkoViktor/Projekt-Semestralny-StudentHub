using System.ComponentModel.DataAnnotations;

namespace StudentHub.Api.Models.Auth
{
    public class RegisterDto
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Role { get; set; } 
    }
}