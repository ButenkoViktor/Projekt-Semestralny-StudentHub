using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Core.Entities.Tasks
{
    public class TaskSubmissionFile
    {
        /// <summary>
        /// Primary key of the task submission file.
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
        /// Primary key of the associated task submission.
        /// </summary>
        public int SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets the task submission associated with this instance.
        /// </summary>
        public TaskSubmission Submission { get; set; } = default!;
    }
}
