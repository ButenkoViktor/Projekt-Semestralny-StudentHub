using System.ComponentModel.DataAnnotations;

namespace StudentHub.Api.Models.Events
{
    public class CreateEventDto
    {
        [Required]
        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? GroupId { get; set; }
    }
}
