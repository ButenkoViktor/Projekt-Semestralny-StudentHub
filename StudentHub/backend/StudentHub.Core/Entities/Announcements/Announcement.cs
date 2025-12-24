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
        /// Gets or sets the unique identifier for the entity.
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
        /// Gets or sets a value indicating whether the announcement is published.
        /// </summary>
        public bool Published { get; set; } = true;

        /// <summary>
        /// Foreign key referencing the Group entity.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Gets or sets the target group for the announcement.
        /// </summary>
        public string? TargetGroup { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the author who created the announcement.
        /// </summary>
        public string AuthorId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the author who created the announcement.
        /// </summary>
        public ApplicationUser Author { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date and time when the object was created, in UTC.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}