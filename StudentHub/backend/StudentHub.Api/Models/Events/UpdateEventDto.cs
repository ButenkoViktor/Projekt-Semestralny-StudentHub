namespace StudentHub.Api.Models.Events
{
    public class UpdateEventDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? GroupId { get; set; }
    }
}
