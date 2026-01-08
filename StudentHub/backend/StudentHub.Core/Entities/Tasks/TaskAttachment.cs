using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Core.Entities.Tasks
{
    public class TaskAttachment
    {

        public int Id { get; set; }

        public string FilePath { get; set; } = default!;

        public string FileName { get; set; } = default!;

        public int TaskId { get; set; }

        public TaskItem Task { get; set; } = default!;
    }
}
