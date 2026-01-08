namespace StudentHub.Core.Entities.Events
{
    public class Event
    {
        public int Id { get; set; }


        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? GroupId { get; set; }

        public string CreatedById { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
