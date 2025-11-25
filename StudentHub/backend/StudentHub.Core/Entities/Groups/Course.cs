using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHub.Core.Entities.Schedule;
using StudentHub.Core.Entities.Groups;

namespace StudentHub.Core.Entities.Groups
{
    public class Course
    {
        /// <summary>
        /// Primary key of the course.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Gets or sets an optional textual description associated with the object.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of schedule items associated with this course.
        /// </summary>
        public ICollection<Schedule.ScheduleItem>? ScheduleItems { get; set; }
    }

}
