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

        public int Id { get; set; }

        public string UserId { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;

        public int TaskId { get; set; }

        public TaskItem Task { get; set; } = default!;

        public DateTime SubmittedAt { get; set; }

        public string? AnswerText { get; set; }

        public TaskSubmissionStatus Status { get; set; }

        public ICollection<TaskSubmissionFile>? Files { get; set; }
    }
}
