using StudentHub.Core.Entities.Groups;
using StudentHub.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Core.Entities.Grades
{
    public class StudentGrade
    {
        /// <summary>
        /// Primary key of the StudentGrade.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key referencing the student (ApplicationUser).
        /// </summary>
        public string StudentId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the student associated with this entity.
        /// </summary>
        public ApplicationUser Student { get; set; } = default!;

        /// <summary>
        /// Foreign key referencing the course.
        /// </summary>
        public int CourseId { get; set; }
        public Course Course { get; set; } = default!;

        /// <summary>
        /// Foreign key referencing the group.
        /// </summary>
        public int GroupId { get; set; }
        public Group Group { get; set; } = default!;
        
        /// <summary>
        /// Gets or sets the date associated with the current instance.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the grade value.
        /// </summary>
        public int? Grade { get; set; } 

        /// <summary>
        /// Gets or sets a value indicating whether the item is present.
        /// </summary>
        public bool IsPresent { get; set; }
    }
}
