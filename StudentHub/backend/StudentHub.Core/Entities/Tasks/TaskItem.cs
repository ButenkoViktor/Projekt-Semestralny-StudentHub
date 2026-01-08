using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHub.Core.Entities.Groups;

namespace StudentHub.Core.Entities.Tasks
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string? Description { get; set; }

        public bool IsPublished { get; set; } = true;

        public int CourseId { get; set; }

        public DateTime Deadline { get; set; }

        public int? GroupId { get; set; }

        public ICollection<TaskAttachment>? Attachments { get; set; }

        public ICollection<TaskSubmission>? Submissions { get; set; }
    }
}
