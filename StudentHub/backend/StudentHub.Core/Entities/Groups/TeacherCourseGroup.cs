using StudentHub.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Core.Entities.Groups
{
    public class TeacherCourseGroup
    {
        /// <summary>
        /// Primary key of the TeacherCourseGroup.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key referencing the teacher (ApplicationUser).
        /// </summary>
        public string TeacherId { get; set; } = default!;
        /// <summary>
        /// Gets or sets the teacher associated with this entity.
        /// </summary>
        public ApplicationUser Teacher { get; set; } = default!;

        /// <summary>
        /// Foreign key referencing the course.
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Gets or sets the course associated with this entity.
        /// </summary>
        public Course Course { get; set; } = default!;

        /// <summary>
        /// Gets or sets the unique identifier for the group associated with this entity.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the group associated with this instance.
        /// </summary>
        public Group Group { get; set; } = default!;
    }
}
