using System;
namespace StudentHub.Core.Entities.Events
{
    public class Event
    {
        /// <summary>
        /// Primary key of the event.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the title associated with the object.
        /// </summary>
        public string Title { get; set; } = default!;
        /// <summary>
        /// Gets or sets the descriptive text associated with the object.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Gets or sets the scheduled start date and time for the operation.
        /// </summary>
        public DateTime StartAt { get; set; }
        /// <summary>
        /// Gets or sets the date and time at which the event or period ends.
        /// </summary>
        public DateTime EndAt { get; set; }
        /// <summary>
        /// Gets or sets the location associated with the object.
        /// </summary>
        public string? Location { get; set; }
    }
}