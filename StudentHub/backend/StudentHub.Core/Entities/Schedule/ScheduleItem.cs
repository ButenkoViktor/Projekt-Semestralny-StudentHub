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
        /// <summary>
        /// Primary key of the schedule item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the course. Foreign key referencing the Course entity.
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Gets or sets the course associated with this entity.
        /// </summary>
        public Course Course { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name of the teacher associated with this instance.
        /// </summary>
        public string TeacherName { get; set; } = default!;

        /// <summary>
        /// Gets or sets the scheduled start time for the operation.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time for the event or operation.
        /// </summary>
        public DateTime EndTime { get; set; }


        /// <summary>
        /// Gets or sets the identifier of the group associated with this entity. Foreign key referencing the Group entity.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Gets or sets the group associated with this instance.
        /// </summary>
        public Group? Group { get; set; }

        /// <summary>
        /// Gets or sets the type of the lesson.
        /// </summary>
        public string? LessonType { get; set; }
    }
}
