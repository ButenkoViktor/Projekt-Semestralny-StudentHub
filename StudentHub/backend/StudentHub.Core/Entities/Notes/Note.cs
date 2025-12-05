using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Notes
{
    public class Note
    {
        /// <summary>
        /// Primary key of the note.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title associated with the object.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Gets or sets the full path to the file associated with this instance.
        /// </summary>
        public string FilePath { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name of the file associated with this instance.
        /// </summary>
        public string FileName { get; set; } = default!;

        /// <summary>
        /// Gets or sets the unique identifier of the user who uploaded the note.
        /// </summary>
        public string UploadedById { get; set; } = default!;

        /// <summary>
        /// Gets or sets the user who uploaded the associated content.
        /// </summary>
        public ApplicationUser UploadedBy { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date and time, in UTC, when the item was uploaded.
        /// </summary>
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
