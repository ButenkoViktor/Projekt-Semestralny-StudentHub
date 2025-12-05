using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Tasks
{
    public class TaskSubmissionFile
    {
        /// <summary>
        /// Primary key of the task submission file.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the file path associated with the operation.
        /// </summary>
        public string FilePath { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name of the file, including its extension.
        /// </summary>
        public string FileName { get; set; } = default!;

        /// <summary>
        /// Foreign key referencing the associated task submission.
        /// </summary>
        public int SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets the task submission associated with the current operation.
        /// </summary>
        public TaskSubmission Submission { get; set; } = default!;
    }
}