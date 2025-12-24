namespace StudentHub.Core.Entities.Events
{
    public class Event
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title associated with the object.
        /// </summary>
        public string Title { get; set; } = default!;
        
        /// <summary>
        /// Gets or sets the description associated with this instance.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Gets or sets the start date for the event or operation.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the event or time period.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the group associated with the entity.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who created the entity.
        /// </summary>
        public string CreatedById { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date and time when the object was created, in Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
