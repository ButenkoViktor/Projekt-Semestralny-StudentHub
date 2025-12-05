using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Announcements
{
    public class Announcement
    {
        /// <summary>
        /// Primary key of the announcement.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title associated with the object.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Gets or sets the textual content associated with this instance.
        /// </summary>
        public string Content { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name of the target group to which the operation applies.
        /// </summary>
        public string? TargetGroup { get; set; } // e.g. "All", "Group-21", “Teachers”

        /// <summary>
        /// Gets or sets the unique identifier of the author associated with this entity. 
        /// </summary>
        public string AuthorId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the author of the application content.
        /// </summary>
        public ApplicationUser Author { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date and time when the object was created, in UTC.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
