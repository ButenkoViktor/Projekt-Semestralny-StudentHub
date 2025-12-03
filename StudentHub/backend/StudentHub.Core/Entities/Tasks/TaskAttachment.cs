using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Core.Entities.Tasks
{
    public class TaskAttachment
    {
        /// <summary>
        /// Primary key of the task attachment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full path to the file associated with this instance.
        /// </summary>
        public string FilePath { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name of the file associated with this instance.
        /// </summary>
        public string FileName { get; set; } = default!;

        /// <summary>
        /// Foreign key referencing the associated task.
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// Gets or sets the asynchronous operation associated with this instance.
        /// </summary>
        public TaskItem Task { get; set; } = default!;

        /// <summary>
        /// Gets or sets the unique identifier for the task item.
        /// </summary>
        public int TaskItemId { get; set; }
        /// <summary>
        /// Gets or sets the URL of the file.
        /// </summary>
        public string FileUrl { get; set; } = default!;

    }
}
