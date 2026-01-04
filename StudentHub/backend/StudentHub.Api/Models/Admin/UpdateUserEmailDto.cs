using System.ComponentModel.DataAnnotations;

namespace StudentHub.Api.Models.Admin
{
    public class UpdateUserEmailDto
    {
        [Required]
        public string UserId { get; set; } = default!;

        [Required, EmailAddress]
        public string NewEmail { get; set; } = default!;
    }
}