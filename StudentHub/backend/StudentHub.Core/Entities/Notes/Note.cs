using System;

namespace StudentHub.Core.Entities.Notes
{
    public class Note
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user associated with this instance.
        /// </summary>
        public string UserId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the title associated with the object.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Gets or sets the textual content associated with this instance.
        /// </summary>
        public string Content { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date and time when the object was created, in Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
