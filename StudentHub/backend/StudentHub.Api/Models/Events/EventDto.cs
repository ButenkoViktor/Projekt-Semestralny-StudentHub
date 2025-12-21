namespace StudentHub.Api.Models.Events
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? GroupId { get; set; }
    }
}
