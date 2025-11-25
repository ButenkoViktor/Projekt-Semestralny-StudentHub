using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHub.Core.Entities.Groups;
using StudentHub.Core.Entities.Users;

namespace StudentHub.Core.Entities.Tasks
{
    public class Task
    {
        /// <summary>
        /// Primary key of the task.
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
        /// Gets or sets the date by which the item is due.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is published and visible to users.
        /// </summary>
        public bool IsPublished { get; set; } = true;

        /// <summary>
        /// Gets or sets the collection of attachments associated with the task.
        /// </summary>
        /// <remarks>The collection may be null if no attachments have been added. Modifying the
        /// collection does not automatically persist changes; ensure that updates are saved as appropriate in your
        /// application workflow.</remarks>
        public ICollection<TaskAttachment>? Attachments { get; set; }
        /// <summary>
        /// Gets or sets the collection of task submissions associated with this entity.
        /// </summary>
        public ICollection<TaskSubmission>? Submissions { get; set; }
    }
}
