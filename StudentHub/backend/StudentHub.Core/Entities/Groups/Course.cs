using StudentHub.Core.Entities.Groups;
using StudentHub.Core.Entities.Schedule;
using StudentHub.Core.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Gets or sets the unique identifier of the teacher associated with this entity.
        /// </summary>
        public string TeacherId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the collection of schedule items associated with the current instance.
        /// </summary>
        public ICollection<ScheduleItem>? ScheduleItems { get; set; }

        /// <summary>
        /// Gets or sets the collection of tasks associated with the current entity.
        /// </summary>
        public ICollection<TaskItem>? Tasks { get; set; }
    }

}
