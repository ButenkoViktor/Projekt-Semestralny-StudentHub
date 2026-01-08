using StudentHub.Core.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Core.Entities.Schedule
{
    public class ScheduleItem
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; } = default!;

        public string TeacherName { get; set; } = default!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int? GroupId { get; set; }

        public Group? Group { get; set; }

        public string? LessonType { get; set; }
    }
}
