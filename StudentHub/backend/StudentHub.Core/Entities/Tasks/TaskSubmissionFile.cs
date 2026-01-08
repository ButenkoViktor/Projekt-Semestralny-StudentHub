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

        public int Id { get; set; }

        public string FilePath { get; set; } = default!;

        public string FileName { get; set; } = default!;

        public int SubmissionId { get; set; }

        public TaskSubmission Submission { get; set; } = default!;
    }
}