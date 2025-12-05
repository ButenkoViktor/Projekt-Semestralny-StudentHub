using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHub.Core.Entities.Tasks;
using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Tasks
{
    public class TaskSubmission
    {
        /// <summary>
        /// Primary key of the task submission.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user associated with this instance. Foreign key referencing the User entity.
        /// </summary>
        public string UserId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the user associated with the current context.
        /// </summary>
        public ApplicationUser User { get; set; } = default!;

        /// <summary>
        /// Gets or sets the unique identifier for the task. Foreign key referencing the Task entity.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Gets or sets the asynchronous operation associated with this instance.
        /// </summary>
        public TaskItem Task { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date and time when the submission was made.
        /// </summary>
        public DateTime SubmittedAt { get; set; }

        /// <summary>
        /// Gets or sets the text of the answer.
        /// </summary>
        public string? AnswerText { get; set; }

        /// <summary>
        /// Gets or sets the collection of files associated with the task submission.
        /// </summary>
        /// <remarks>The collection may be null if no files have been provided. Modifying the collection
        /// does not automatically persist changes; ensure that updates are saved as appropriate for the application's
        /// workflow.</remarks>
        public ICollection<TaskSubmissionFile>? Files { get; set; }
    }
}
