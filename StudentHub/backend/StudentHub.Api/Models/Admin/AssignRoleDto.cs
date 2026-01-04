using System.ComponentModel.DataAnnotations;

namespace StudentHub.Api.Models.Admin
{
    public class AssignRoleDto
    {
        [Required]
        public string UserId { get; set; } = default!;

        [Required]
        public string Role { get; set; } = default!;
    }
}